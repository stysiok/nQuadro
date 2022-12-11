using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NQuadro.Monitors.Channels;
using NQuadro.Monitors.Feeds;
using NQuadro.Monitors.Storages;
using NQuadro.Notifications.Models.Events;
using NQuadro.Shared.Messaging;

namespace NQuadro.Monitors.HostedServices
{
    internal sealed class AssetAddedEventListener : BackgroundService
    {
        private readonly IMessageReceiver _receiver;
        private readonly AssetMonitorChannel _channel;
        private readonly IAssetPricingFeed _assetPricingFeed;
        private readonly IAssetMonitorsStorage _assetMonitorsStorage;
        private readonly ILogger<AssetAddedEventListener> _logger;

        public AssetAddedEventListener(IMessageReceiver receiver, AssetMonitorChannel channel, IAssetPricingFeed assetPricingFeed, IAssetMonitorsStorage assetMonitorsStorage, ILogger<AssetAddedEventListener> logger)
        {
            _receiver = receiver;
            _channel = channel;
            _assetPricingFeed = assetPricingFeed;
            _assetMonitorsStorage = assetMonitorsStorage;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _receiver.ReceiverAsync<AssetAdded>("asset_added", async (assetAdded) =>
            {
                _logger.LogInformation("Received {name} asset_added event", assetAdded.Name);

                var currentPrice = await _assetPricingFeed.GetAssetPricingAsync(assetAdded.Name);
                if (currentPrice is null)
                    throw new Exception("Asset does not exists in the system");

                await _assetMonitorsStorage.AddAssetMonitorAsync(assetAdded.Name, assetAdded.Change, currentPrice.Price, assetAdded.End, false);

                _logger.LogInformation("Sending message to internal monitoring channel");
                await _channel.Broker.Writer.WriteAsync(new(assetAdded.Name, StartMonitoring: true));
            });
        }
    }
}
