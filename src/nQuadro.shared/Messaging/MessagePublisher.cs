using NQuadro.Shared.Serialization;
using StackExchange.Redis;

namespace NQuadro.Shared.Messaging;

internal sealed class MessagePublisher : IMessagePublisher
{
    private readonly ISubscriber _publisher;
    private readonly ISerialization _serialization;

    public MessagePublisher(IConnectionMultiplexer connectionMultiplexer, ISerialization serialization)
    {
        _publisher = connectionMultiplexer.GetSubscriber();
        _serialization = serialization;
    }

    public async Task PublishAsync<T>(string topic, T message)
    {
        var serializedData = _serialization.Serialize(message);
        await _publisher.PublishAsync(topic, serializedData);
    }
}
