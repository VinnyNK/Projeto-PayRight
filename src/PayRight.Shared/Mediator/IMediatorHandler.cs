using PayRight.Shared.Commands;
using PayRight.Shared.EventNotifications;

namespace PayRight.Shared.Mediator;

public interface IMediatorHandler
{
    Task<ICommandResult> EnviarComando<T>(T comando) where T : ICommand;
    
    Task PublicarEvento<T>(T evento) where T : ICustomNotification;
}