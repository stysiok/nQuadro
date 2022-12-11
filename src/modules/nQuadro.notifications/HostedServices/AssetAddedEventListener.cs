using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NQuadro.Notifications.Models.Events;
using NQuadro.Notifications.Storages;
using NQuadro.Shared.Messaging;

namespace NQuadro.Notifications.HostedServices
{
    internal sealed class AssetAddedEventListener : BackgroundService
    {
        private readonly IMessageReceiver _receiver;
        private readonly ILogger<AssetAddedEventListener> _logger;
        private readonly IAssetNotificationsStorage _assetNotificationsStorage;

        public AssetAddedEventListener(IMessageReceiver receiver, ILogger<AssetAddedEventListener> logger, IAssetNotificationsStorage assetNotificationsStorage)
        {
            _receiver = receiver;
            _logger = logger;
            _assetNotificationsStorage = assetNotificationsStorage;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _receiver.ReceiverAsync<AssetAdded>("asset_added", (data) =>
            {
                _logger.LogInformation("New asset added to the system {name}", data.Name);
                _assetNotificationsStorage.AddNotifications(data.Name, new(false, false, false));
            });
        }
    }
}
