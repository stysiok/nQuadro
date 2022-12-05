using Microsoft.Extensions.DependencyInjection;

namespace NQuadro.Shared.Messaging;

public static class MessagingExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IMessagePublisher, MessagePublisher>()
            .AddSingleton<IMessageReceiver, MessageReceiver>();
}
