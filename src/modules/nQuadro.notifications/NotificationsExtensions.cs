using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NQuadro.Notifications.HostedServices;
using NQuadro.Notifications.Services;
using NQuadro.Notifications.Services.SendGrid;
using NQuadro.Notifications.Storages;

namespace NQuadro.Notifications;

public static class NotificationsExtensions
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection serviceCollection, IConfiguration configuration)
        => serviceCollection
            .AddHostedService<AssetAddedEventListener>()
            .AddHostedService<AssetValueChangedEventListener>()
            .AddHostedService<AssetDeletedEventListener>()
            .AddSingleton<IAssetNotificationsStorage, AssetNotificationsStorage>()
            .AddEmailNotificationService(configuration)
            .AddSingleton<INotificationService, SMSNotificationService>()
            .AddSingleton<INotificationService, SlackNotificationService>();
}
