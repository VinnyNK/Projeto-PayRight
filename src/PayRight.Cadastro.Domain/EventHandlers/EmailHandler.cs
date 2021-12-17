using System.Text.Json;
using PayRight.Cadastro.Domain.EventNotifications;
using PayRight.Shared.EventHandlers;

namespace PayRight.Cadastro.Domain.EventHandlers;

public class EmailHandler : ICustomNotificationHandler<UsuarioCriadoNotification>
{
    public Task Handle(UsuarioCriadoNotification notification, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            Console.WriteLine(JsonSerializer.Serialize(notification));
        }, cancellationToken);
    }
}