namespace NQuadro.Shared.Messaging;

public interface IMessageReceiver
{
    Task ReceiverAsync<T>(string topic, Action<T> onMessageReceived);
}
