using Microsoft.Extensions.DependencyInjection;
using NQuadro.Monitors.Channels;
using NQuadro.Monitors.HostedServices;
using NQuadro.Monitors.Services;

namespace NQuadro.Monitors;

public static class MonitorsExtensions
{
    public static IServiceCollection AddMonitorsModule(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IAssetMonitorService, AssetMonitorService>()
            .AddSingleton<AssetMonitorChannel>()
            .AddHostedService<AssetAddedEventListener>()
            .AddHostedService<AssetMonitorHostedService>();

}
