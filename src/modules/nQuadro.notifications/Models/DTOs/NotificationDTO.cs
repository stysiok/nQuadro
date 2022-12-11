namespace NQuadro.Notifications.Models.DTOs;

public sealed record NotificationDTO(bool Email, bool Sms, bool Slack);