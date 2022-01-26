using Flunt.Validations;
using PayRight.Shared.Utils.Extentions;

namespace PayRight.Extrato.Domain.Entities;

public class ContaCorrenteExtrato : Extrato
{
    public Guid ContaCorrenteId { get; }

    public ContaCorrenteExtrato(Guid contaCorrenteId, Guid usuarioId, int mes, int ano) : base(usuarioId, mes, ano)
    {
        ContaCorrenteId = contaCorrenteId;
        
        Validar();
    }
    
    protected ContaCorrenteExtrato() : base(Guid.Empty, 0, 0)
    {}

    public sealed override void Validar()
    {
        AddNotifications(
            new Contract<ContaCorrenteExtrato>()
                .Requires()
                .IsGuidNotEmpty(ContaCorrenteId, "ContaCorrenteId", "Id da Conta Corrente deve ser informado")
                .IsGuidNotEmpty(UsuarioId, "UsuarioId", "Id do Usuario deve ser informado")
                .Join(PeriodoExtrato)
            );
    }
}