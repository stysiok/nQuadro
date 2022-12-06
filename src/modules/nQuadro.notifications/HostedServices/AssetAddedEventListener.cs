using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NQuadro.Notifications.Models.Events;
using NQuadro.Shared.Messaging;

namespace NQuadro.Notifications.HostedServices
{
    internal sealed class AssetAddedEventListener : BackgroundService
    {
        private readonly IMessageReceiver _receiver;
        private readonly ILogger<AssetAddedEventListener> _logger;

        public AssetAddedEventListener(IMessageReceiver receiver, ILogger<AssetAddedEventListener> logger)
        {
            _receiver = receiver;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _receiver.ReceiverAsync<AssetAdded>("asset_added", (data) =>
            {
                _logger.LogInformation("New asset added to the system {name}", data.Name);
            });
        }
    }
}
