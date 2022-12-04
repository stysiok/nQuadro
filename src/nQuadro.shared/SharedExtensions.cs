using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NQuadro.Shared.CQRS;
using NQuadro.Shared.Redis;
using NQuadro.Shared.Serialization;
using NQuadro.Shared.Storage;

namespace NQuadro.Shared;

public static class SharedExtensions
{
    public static IServiceCollection AddSharedComponents(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddControllers();
        return serviceCollection
            .AddRedis(configuration)
            .AddStorage()
            .AddSerialization()
            .AddCQRS();
    }
}