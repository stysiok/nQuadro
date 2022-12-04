using Microsoft.Extensions.DependencyInjection;

namespace NQuadro.Shared.Storage;

public static class StorageExtensions
{
    public static IServiceCollection AddStorage(this IServiceCollection serviceCollection)
        => serviceCollection.AddSingleton<IStorage, Storage>();
}
