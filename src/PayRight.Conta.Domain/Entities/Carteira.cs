using Flunt.Validations;

namespace PayRight.Conta.Domain.Entities;

public class Carteira : Conta
{
    public decimal Saldo { get; private set; }
    
    public Carteira(Guid usuarioId) : base(usuarioId, "Carteira", null)
    {
        Saldo = 0;
        
        Validar();
    }
    
    protected Carteira() : base(Guid.Empty, "", null)
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
                .IsGreaterOrEqualsThan(Saldo, 0, "Carteira.Saldo", "Saldo nao pode ser menor que zero")
                .Join(NomeConta)
        );
    }
}