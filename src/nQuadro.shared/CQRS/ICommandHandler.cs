namespace NQuadro.Shared.CQRS;

public interface ICommandHandler<T> where T : ICommand
{
    Task HandleAsync(T command);
}
