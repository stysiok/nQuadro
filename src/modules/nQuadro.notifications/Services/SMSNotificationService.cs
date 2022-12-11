using Microsoft.Extensions.Logging;
using NQuadro.Notifications.Models;

namespace NQuadro.Notifications.Services;

internal sealed class SMSNotificationService : INotificationService
{
    private readonly ILogger<SMSNotificationService> _logger;

    public SMSNotificationService(ILogger<SMSNotificationService> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(NotificationData notificationData)
    {
        _logger.LogInformation("Notyfing sms");
        return Task.CompletedTask;
    }
}
