using PayRight.Extrato.Domain.Entities;

namespace PayRight.Extrato.Domain.Repositories;

public interface IContaCorrenteExtratoLeituraRepository
{
    Task<ContaCorrenteExtrato?> BuscarExtratoPorMesEAno(Guid contaCorrenteId, Guid usuarioId, int mes, int ano);

    Task<bool> VerificaSeContaCorrenteEhDoUsuario(Guid contaCorrenteId, Guid usuarioId);
}