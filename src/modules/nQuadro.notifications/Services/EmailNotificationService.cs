using Microsoft.Extensions.Logging;
using NQuadro.Notifications.Models;

namespace NQuadro.Notifications.Services;

internal sealed class EmailNotificationService : INotificationService
{
    private readonly ILogger<EmailNotificationService> _logger;

    public EmailNotificationService(ILogger<EmailNotificationService> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(NotificationData notificationData)
    {
        _logger.LogInformation("Notyfing email");
        return Task.CompletedTask;
    }
}
