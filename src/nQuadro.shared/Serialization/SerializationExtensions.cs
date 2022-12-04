using Microsoft.Extensions.DependencyInjection;

namespace NQuadro.Shared.Serialization;

public static class SerializationExtensions
{
    public static IServiceCollection AddSerialization(this IServiceCollection serviceCollection)
        => serviceCollection.AddSingleton<ISerialization, Serialization>();
}
