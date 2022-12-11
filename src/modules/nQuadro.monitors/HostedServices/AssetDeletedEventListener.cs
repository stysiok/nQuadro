using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NQuadro.Monitors.Channels;
using NQuadro.Monitors.Feeds;
using NQuadro.Monitors.Storages;
using NQuadro.Notifications.Models.Events;
using NQuadro.Shared.Messaging;

namespace NQuadro.Monitors.HostedServices
{
    internal sealed class AssetDeletedEventListener : BackgroundService
    {
        private readonly IMessageReceiver _receiver;
        private readonly AssetMonitorChannel _channel;
        private readonly IAssetMonitorsStorage _assetMonitorsStorage;
        private readonly ILogger<AssetDeletedEventListener> _logger;

        public AssetDeletedEventListener(IMessageReceiver receiver, AssetMonitorChannel channel, IAssetMonitorsStorage assetMonitorsStorage, ILogger<AssetDeletedEventListener> logger)
        {
            _receiver = receiver;
            _channel = channel;
            _assetMonitorsStorage = assetMonitorsStorage;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _receiver.ReceiverAsync<AssetDeleted>("asset_deleted", async (assetDeleted) =>
            {
                _logger.LogInformation("Received {name} asset_deleted event", assetDeleted.Name);

                await _assetMonitorsStorage.DeleteAssetMonitorAsync(assetDeleted.Name);

                _logger.LogInformation("Deleted {name} from monitoring", assetDeleted.Name);
            });
        }
    }
}
