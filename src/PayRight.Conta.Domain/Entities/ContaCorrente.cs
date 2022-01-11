using Flunt.Validations;

namespace PayRight.Conta.Domain.Entities;

public class ContaCorrente : Conta
{
    public decimal Saldo { get; private set; }

    public ContaCorrente(Guid usuarioId, string nome, string? apelido) : base(usuarioId, nome, apelido)
    {
        Saldo = 0;
        
        Validar();
    }
    
    protected ContaCorrente() : base(Guid.Empty, "", null)
    { }

    public void SomarSaldo(decimal valor)
    {
        Saldo += valor;
        Validar();
    }

    public void SubtrairSaldo(decimal valor)
    {
        Saldo -= valor;
        Validar();
    }

    public sealed override void Validar()
    {
        AddNotifications(
            new Contract<ContaCorrente>()
                .Requires()
                .Join(NomeConta)
        );
    }
}