using Microsoft.Extensions.DependencyInjection;
using NQuadro.Assets.Storages;

namespace NQuadro.Assets;

public static class AssetsExtensions
{
    public static IServiceCollection AddAssetsModule(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddTransient<IAssetsStorage, AssetsStorage>();
}
