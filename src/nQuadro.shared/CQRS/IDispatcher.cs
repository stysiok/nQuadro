namespace NQuadro.Shared.CQRS;

public interface IDispatcher
{
    Task SendAsync<T>(T command) where T : ICommand;
}
