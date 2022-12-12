namespace NQuadro.Notifications.Models.Options;

internal sealed class SendGridOptions
{
    public string ApiKey { get; init; } = "";
    public string FromEmail { get; init; } = "";
    public string FromName { get; init; } = "";
    public string SendTo { get; init; } = "";

}