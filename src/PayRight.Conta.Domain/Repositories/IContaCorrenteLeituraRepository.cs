using PayRight.Conta.Domain.Entities;

namespace PayRight.Conta.Domain.Repositories;

public interface IContaCorrenteLeituraRepository
{
    public Task<bool> NomeContaExisteParaUsuario(Guid usuarioId, string nome);

    public Task<IEnumerable<ContaCorrente>> BuscarContasCorrente(Guid usuarioId);

    public Task<ContaCorrente?> BuscaContaCorrente(Guid usuarioId, Guid contaCorrenteId);
}