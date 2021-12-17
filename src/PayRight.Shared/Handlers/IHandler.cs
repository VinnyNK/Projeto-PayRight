using MediatR;
using PayRight.Shared.Commands;

namespace PayRight.Shared.Handlers;

public interface IHandler<T> : IRequestHandler<T, ICommandResult> where T : ICommand
{
    new Task<ICommandResult> Handle(T command, CancellationToken cancellationToken);
}