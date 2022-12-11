using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NQuadro.Notifications.Models;
using NQuadro.Notifications.Models.Events;
using NQuadro.Notifications.Services;
using NQuadro.Shared.Messaging;

namespace NQuadro.Notifications.HostedServices
{
    internal sealed class AssetValueChangedEventListener : BackgroundService
    {
        private readonly IMessageReceiver _receiver;
        private readonly IEnumerable<INotificationService> _notificationServices;
        private readonly ILogger<AssetValueChangedEventListener> _logger;

        public AssetValueChangedEventListener(IMessageReceiver receiver, IEnumerable<INotificationService> notificationServices, ILogger<AssetValueChangedEventListener> logger)
        {
            _receiver = receiver;
            _notificationServices = notificationServices;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _receiver.ReceiverAsync<AssetValueChangedEvent>("asset_value_changed", (data) =>
            {
                _logger.LogInformation("Received asset_value_changed event for {name}", data.Name);
                var notificationData = new NotificationData(data.Name, data.Change);
                foreach (var notificationService in _notificationServices)
                {
                    _ = notificationService.SendAsync(notificationData);
                }
                _logger.LogInformation("Triggered all notification services");
            });
        }
    }
}
