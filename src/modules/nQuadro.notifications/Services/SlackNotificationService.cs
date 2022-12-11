using Microsoft.Extensions.Logging;
using NQuadro.Notifications.Models;

namespace NQuadro.Notifications.Services;

internal sealed class SlackNotificationService : INotificationService
{
    private readonly ILogger<SlackNotificationService> _logger;

    public SlackNotificationService(ILogger<SlackNotificationService> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(NotificationData notificationData)
    {
        _logger.LogInformation("Notyfing slack");
        return Task.CompletedTask;
    }
}
