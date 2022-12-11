using NQuadro.Notifications.Storages;
using NQuadro.Notifications.Models;
using NQuadro.Shared.CQRS;

namespace NQuadro.Notifications.Logic.Notifications.Commands.CommandHandlers;

internal sealed class SetNotificationsHandler : ICommandHandler<SetNotifications>
{
    private readonly IAssetNotificationsStorage _notificationsStorage;

    public SetNotificationsHandler(IAssetNotificationsStorage notificationsStorage)
    {
        _notificationsStorage = notificationsStorage;
    }

    public async Task HandleAsync(SetNotifications command)
    {
        NotificationsSettings notifications = new(command.Notifications.Email, command.Notifications.Sms, command.Notifications.Slack);
        await _notificationsStorage.AddNotifications(command.AssetName, notifications);
    }
}
