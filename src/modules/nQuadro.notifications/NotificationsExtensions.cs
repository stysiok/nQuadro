using Microsoft.Extensions.DependencyInjection;
using NQuadro.Notifications.HostedServices;

namespace NQuadro.Notifications;

public static class NotificationsExtensions
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddHostedService<AssetAddedEventListener>()
            .AddHostedService<AssetValueChangedEventListener>();
}
