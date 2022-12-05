using NQuadro.Shared.Serialization;
using StackExchange.Redis;

namespace NQuadro.Shared.Messaging;

internal sealed class MessageReceiver : IMessageReceiver
{
    private readonly ISubscriber _publisher;
    private readonly ISerialization _serialization;

    public MessageReceiver(IConnectionMultiplexer connectionMultiplexer, ISerialization serialization)
    {
        _publisher = connectionMultiplexer.GetSubscriber();
        _serialization = serialization;
    }

    public Task ReceiverAsync<T>(string topic, Action<T> onMessageReceived)
    {
        return _publisher.SubscribeAsync(topic, (channel, value) =>
        {
            var data = _serialization.Deserialize<T>(value);
            if (data is null) return;

            onMessageReceived(data);
        });
    }
}
