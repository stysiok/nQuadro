using NQuadro.Notifications.Models.DTOs;
using NQuadro.Shared.CQRS;

namespace NQuadro.Notifications.Logic.Notifications.Queries;

internal sealed record GetNotifications(string AssetName) : IQuery<NotificationDTO>;