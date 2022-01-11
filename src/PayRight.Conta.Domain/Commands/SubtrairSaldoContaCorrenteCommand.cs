using Flunt.Notifications;
using Flunt.Validations;
using PayRight.Shared.Commands;
using PayRight.Shared.Utils.Extentions;

namespace PayRight.Conta.Domain.Commands;

public class SubtrairSaldoContaCorrenteCommand : Notifiable<Notification>, ICommand
{
    public Guid UsuarioId { get; }

    public Guid ContaCorrenteId { get; }

    public decimal Valor { get; }

    public SubtrairSaldoContaCorrenteCommand(Guid usuarioId, Guid contaCorrenteId, decimal valor)
    {
        UsuarioId = usuarioId;
        ContaCorrenteId = contaCorrenteId;
        Valor = valor;
    }

    public void Validar()
    {
        AddNotifications(
            new Contract<SomaSaldoContaCorrenteCommand>()
                .Requires()
                .IsGuidNotEmpty(UsuarioId, "UsuarioId", "Id do usuário deve ser informado")
                .IsGuidNotEmpty(ContaCorrenteId, "ContaCorrenteId", "Id da Conta Corrente deve ser informada")
                .IsGreaterThan(Valor, 0, "Valor", "Valor deve ser maior que Zero")
        );
    }
}