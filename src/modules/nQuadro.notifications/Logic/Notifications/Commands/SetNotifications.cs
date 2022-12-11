using NQuadro.Notifications.Models.DTOs;
using NQuadro.Shared.CQRS;

namespace NQuadro.Notifications.Logic.Notifications.Commands;

internal sealed record SetNotifications(string AssetName, NotificationDTO Notifications) : ICommand;