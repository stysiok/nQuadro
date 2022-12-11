using NQuadro.Notifications.Models;

namespace NQuadro.Notifications.Storages;

internal interface IAssetNotificationsStorage
{
    Task AddNotifications(string assetName, NotificationsSettings notifications);
    Task<NotificationsSettings> GetNotifications(string assetName);
}
