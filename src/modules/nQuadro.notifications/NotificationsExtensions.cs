using Microsoft.Extensions.DependencyInjection;
using NQuadro.Notifications.HostedServices;
using NQuadro.Notifications.Storages;

namespace NQuadro.Notifications;

public static class NotificationsExtensions
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddHostedService<AssetAddedEventListener>()
            .AddHostedService<AssetValueChangedEventListener>()
            .AddHostedService<AssetDeletedEventListener>()
            .AddSingleton<IAssetNotificationsStorage, AssetNotificationsStorage>();
}
