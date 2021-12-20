using Flunt.Notifications;

namespace PayRight.Shared.Commands;

public class CommandResult : Notifiable<Notification>, ICommandResult
{
    public bool Sucesso { get; }
    public string Mensagem { get;  }
    public IReadOnlyCollection<Notification> CommandNotifications => Notifications;

    public CommandResult(bool sucesso, string mensagem)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
    }
    
    public CommandResult(bool sucesso, string mensagem, IReadOnlyCollection<Notification> notifications)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
        AddNotifications(notifications);
    }
}
