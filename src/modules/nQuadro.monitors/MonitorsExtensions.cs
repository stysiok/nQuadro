using Microsoft.Extensions.DependencyInjection;
using NQuadro.Monitors.HostedServices;

namespace NQuadro.Monitors;

public static class MonitorsExtensions
{
    public static IServiceCollection AddMonitorsModule(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddHostedService<AssetAddedBackgroundService>();

}
