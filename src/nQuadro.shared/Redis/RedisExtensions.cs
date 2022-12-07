using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NQuadro.Shared.Redis.Models;
using StackExchange.Redis;

namespace NQuadro.Shared.Redis;

public static class RedisExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var redisSection = configuration.GetSection("Redis");
        if (redisSection is null) throw new Exception("Missing redis configuration section");

        var redisOptions = new RedisOptions();
        redisSection.Bind(redisOptions);

        if (redisOptions.ConnectionString is null) throw new Exception("Missing redis configuration section");

        serviceCollection.Configure<RedisOptions>(redisSection);

        serviceCollection.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisOptions.ConnectionString));

        return serviceCollection;
    }
}
