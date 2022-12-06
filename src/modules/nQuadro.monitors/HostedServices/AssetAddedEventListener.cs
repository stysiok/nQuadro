using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NQuadro.Monitors.Channels;
using NQuadro.Notifications.Models.Events;
using NQuadro.Shared.Messaging;

namespace NQuadro.Monitors.HostedServices
{
    internal sealed class AssetAddedEventListener : BackgroundService
    {
        private readonly IMessageReceiver _receiver;
        private readonly AssetMonitorChannel _channel;
        private readonly ILogger<AssetAddedEventListener> _logger;

        public AssetAddedEventListener(IMessageReceiver receiver, AssetMonitorChannel channel, ILogger<AssetAddedEventListener> logger)
        {
            _receiver = receiver;
            _channel = channel;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _receiver.ReceiverAsync<AssetAdded>("asset_added", async (data) =>
            {
                _logger.LogInformation("Adding new asset {name} to monitoring", data.Name);
                await _channel.Broker.Writer.WriteAsync(new(data.Name, StartMonitoring: true));
                _logger.LogInformation("Added new asset {name} to monitoring", data.Name);
            });
        }
    }
}
