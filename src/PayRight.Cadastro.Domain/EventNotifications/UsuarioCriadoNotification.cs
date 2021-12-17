using PayRight.Cadastro.Domain.ValueObjects;
using PayRight.Shared.EventNotifications;

namespace PayRight.Cadastro.Domain.EventNotifications;

public class UsuarioCriadoNotification : ICustomNotification
{
    public UsuarioCriadoNotification(Guid aggregateId, string primeiroNome, string enderecoEmail)
    {
        AggregateId = aggregateId;
        PrimeiroNome = primeiroNome;
        EnderecoEmail = enderecoEmail;
    }

    public Guid AggregateId { get; }

    public string PrimeiroNome { get; }

    public string EnderecoEmail { get; }
    
}