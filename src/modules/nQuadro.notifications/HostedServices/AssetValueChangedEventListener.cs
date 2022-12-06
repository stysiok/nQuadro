using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NQuadro.Notifications.Models.Events;
using NQuadro.Shared.Messaging;

namespace NQuadro.Notifications.HostedServices
{
    internal sealed class AssetValueChangedEventListener : BackgroundService
    {
        private readonly IMessageReceiver _receiver;
        private readonly ILogger<AssetValueChangedEventListener> _logger;

        public AssetValueChangedEventListener(IMessageReceiver receiver, ILogger<AssetValueChangedEventListener> logger)
        {
            _receiver = receiver;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _receiver.ReceiverAsync<AssetValueChangedEvent>("asset_value_changed", (data) =>
            {
                _logger.LogError("Notifying user about the value of {name} [ch: {value}]", data.Name, data.Change);
            });
        }
    }
}
