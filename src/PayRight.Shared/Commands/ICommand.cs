using Flunt.Notifications;
using MediatR;

namespace PayRight.Shared.Commands;

public interface ICommand : IRequest<ICommandResult>
{
    void Validar();
}