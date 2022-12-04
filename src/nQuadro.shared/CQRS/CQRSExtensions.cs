using Microsoft.Extensions.DependencyInjection;

namespace NQuadro.Shared.CQRS;

public static class CQRSExtensions
{
    public static IServiceCollection AddCQRS(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDispatcher, Dispatcher>();

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        serviceCollection.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        serviceCollection.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        return serviceCollection;
    }
}
