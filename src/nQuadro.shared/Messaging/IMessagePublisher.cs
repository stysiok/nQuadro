namespace NQuadro.Shared.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<T>(string topic, T message);
}
