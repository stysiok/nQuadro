using NQuadro.Notifications.Models.DTOs;
using NQuadro.Notifications.Storages;
using NQuadro.Shared.CQRS;

namespace NQuadro.Notifications.Logic.Notifications.Queries.QueryHandlers;

internal sealed class GetNotificationsHandler : IQueryHandler<GetNotifications, NotificationDTO>
{
    private readonly IAssetNotificationsStorage _notificationsStorage;

    public GetNotificationsHandler(IAssetNotificationsStorage notificationsStorage)
    {
        _notificationsStorage = notificationsStorage;
    }

    public async Task<NotificationDTO> HandleAsync(GetNotifications query)
    {
        var settings = await _notificationsStorage.GetNotificationsAsync(query.AssetName);
        return new NotificationDTO(settings.Email, settings.Sms, settings.Slack);
    }
}
