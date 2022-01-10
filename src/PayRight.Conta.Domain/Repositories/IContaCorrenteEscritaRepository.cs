using PayRight.Conta.Domain.Entities;

namespace PayRight.Conta.Domain.Repositories;

public interface IContaCorrenteEscritaRepository
{
    public Task CriarContaCorrente(ContaCorrente contaCorrente);
    
    Task<bool> Commit();
}