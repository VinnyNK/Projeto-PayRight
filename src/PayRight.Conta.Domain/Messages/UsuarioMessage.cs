using Flunt.Notifications;
using Flunt.Validations;
using PayRight.Shared.Messages;
using PayRight.Shared.Utils.Extentions;

namespace PayRight.Conta.Domain.Messages;

public class UsuarioMessage : Notifiable<Notification>, IEntityMessage
{
    public Guid AggregateId { get; set; }

    public string? PrimeiroNome { get; set; }
    
    public void Validar()
    {
        AddNotifications(
            new Contract<UsuarioMessage>()
                .Requires()
                .IsNotNullOrEmpty(PrimeiroNome, "PrimeiroNome", "Nome não informado na mensagem")
                .IsGuidNotEmpty(AggregateId, "AggregateId", "Id de agregação não informado na mensagem")
            );
    }
}