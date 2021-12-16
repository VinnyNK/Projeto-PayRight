using PayRight.Shared.Commands;

namespace PayRight.Shared.Handlers;

public interface IHandler<T> where T : ICommand
{
    Task<ICommandResult> Handle(T command);
}