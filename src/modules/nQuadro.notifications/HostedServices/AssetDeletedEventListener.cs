using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NQuadro.Notifications.Models.Events;
using NQuadro.Notifications.Storages;
using NQuadro.Shared.Messaging;

namespace NQuadro.Notifications.HostedServices
{
    internal sealed class AssetDeletedEventListener : BackgroundService
    {
        private readonly IMessageReceiver _receiver;
        private readonly ILogger<AssetDeletedEventListener> _logger;
        private readonly IAssetNotificationsStorage _assetNotificationsStorage;

        public AssetDeletedEventListener(IMessageReceiver receiver, ILogger<AssetDeletedEventListener> logger, IAssetNotificationsStorage assetNotificationsStorage)
        {
            _receiver = receiver;
            _logger = logger;
            _assetNotificationsStorage = assetNotificationsStorage;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _receiver.ReceiverAsync<AssetDeleted>("asset_deleted", (data) =>
            {
                _assetNotificationsStorage.DeleteNotificationsAsync(data.Name);
                _logger.LogInformation("Deleted asset {name} from the notifications", data.Name);
            });
        }
    }
}
