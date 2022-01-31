using Flunt.Notifications;
using Flunt.Validations;
using PayRight.Shared.Commands;
using PayRight.Shared.Utils.Extentions;

namespace PayRight.Extrato.Domain.Commands;

public class PagarAtividadeContaCorrenteCommand : Notifiable<Notification>, ICommand
{
    public Guid UsuarioId { get; private set; }
    
    public Guid ContaCorrenteId { get; private set; }
    
    public Guid AtividadeId { get; private set; }

    public DateOnly DataPagamento { get; private set; }

    public PagarAtividadeContaCorrenteCommand(Guid usuarioId, Guid contaCorrenteId, Guid atividadeId, DateOnly dataPagamento)
    {
        UsuarioId = usuarioId;
        ContaCorrenteId = contaCorrenteId;
        AtividadeId = atividadeId;
        DataPagamento = dataPagamento;

        Validar();
    }

    public void Validar()
    {
        AddNotifications(
            new Contract<PagarAtividadeContaCorrenteCommand>()
                .Requires()
                .IsGuidNotEmpty(UsuarioId, "UsuarioId", "Id do Usuario deve ser informado")
                .IsGuidNotEmpty(ContaCorrenteId, "ContaCorrenteId", "Id da Conta Corrente deve ser informado")
                .IsGuidNotEmpty(AtividadeId, "AtividadeId", "Id da Atividade deve ser informado")
        );
    }
}