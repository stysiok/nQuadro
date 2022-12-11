using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NQuadro.Monitors.Channels;
using NQuadro.Monitors.Feeds;
using NQuadro.Monitors.HostedServices;
using NQuadro.Monitors.Models.Configurations;
using NQuadro.Monitors.Services;
using NQuadro.Monitors.Storages;

namespace NQuadro.Monitors;

public static class MonitorsExtensions
{
    public static IServiceCollection AddMonitorsModule(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var darqubeSection = configuration.GetSection("Darqube");
        if (darqubeSection is null) throw new Exception("Missing Darqube configuration section");

        return serviceCollection
            .Configure<DarqubeConfiguration>(darqubeSection)
            .AddSingleton<IAssetMonitorService, AssetMonitorService>()
            .AddSingleton<AssetMonitorChannel>()
            .AddSingleton<IAssetMonitorsStorage, AssetMonitorsStorage>()
            .AddSingleton<IAssetPricingFeed, AssetPricingFeed>()
            .AddHostedService<AssetAddedEventListener>()
            .AddHostedService<AssetDeletedEventListener>()
            .AddHostedService<AssetMonitorManager>();
    }

}
