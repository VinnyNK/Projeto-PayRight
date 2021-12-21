using Flunt.Notifications;

namespace PayRight.Shared.Commands;

public interface ICommandResult
{
    public bool Sucesso { get; }

    public string Mensagem { get; }

    public IReadOnlyCollection<Notification> CommandNotifications { get; }
    
}