using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NQuadro.Notifications.Models;
using NQuadro.Notifications.Models.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NQuadro.Notifications.Services.SendGrid;

internal sealed class EmailNotificationService : INotificationService
{
    private readonly ISendGridClient _sendGridClient;
    private readonly ILogger<EmailNotificationService> _logger;
    private readonly SendGridOptions _sendGridOptions;

    public EmailNotificationService(ISendGridClient sendGridClient, IOptions<SendGridOptions> options, ILogger<EmailNotificationService> logger)
    {
        _sendGridClient = sendGridClient;
        _logger = logger;
        _sendGridOptions = options.Value;
    }

    public async Task SendAsync(NotificationData notificationData)
    {
        _logger.LogInformation("Creating notification using {service}", nameof(EmailNotificationService));

        var message = new SendGridMessage
        {
            From = new EmailAddress(_sendGridOptions.FromEmail, _sendGridOptions.FromName),
            Subject = "Asset value has changed",
            PlainTextContent = $"Asset {notificationData.AssetName} value has changed over {notificationData.Change}"
        };
        message.AddTo(_sendGridOptions.SendTo);
        var result = await _sendGridClient.SendEmailAsync(message);

        if (result.IsSuccessStatusCode)
            _logger.LogInformation("Send notification using {service}", nameof(EmailNotificationService));
        else
            _logger.LogError("Something went wrong while sending email using SendGrid in {service}", nameof(EmailNotificationService));
    }
}
