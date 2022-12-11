using NQuadro.Notifications.Models;

namespace NQuadro.Notifications.Services;

internal interface INotificationService
{
    Task SendAsync(NotificationData notificationData);
}
