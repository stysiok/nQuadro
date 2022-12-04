using Microsoft.Extensions.DependencyInjection;

namespace NQuadro.Shared.CQRS;

internal sealed class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<T> QueryAsync<T>(IQuery<T> query)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(T));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        var method = handlerType.GetMethod(nameof(IQueryHandler<IQuery<T>, T>.HandleAsync));

        return await (Task<T>)method.Invoke(handler, new object[] { query });
    }

    public async Task SendAsync<T>(T command) where T : ICommand
    {
        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>();
        await handler.HandleAsync(command);
    }
}
