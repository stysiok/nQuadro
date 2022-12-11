using NQuadro.Notifications.Models;

namespace NQuadro.Notifications.Storages;

internal interface IAssetNotificationsStorage
{
    Task AddNotificationsAsync(string assetName, NotificationsSettings notifications);
    Task<NotificationsSettings> GetNotificationsAsync(string assetName);
    Task DeleteNotificationsAsync(string assetName);
}
