using PayRight.Conta.Domain.Entities;

namespace PayRight.Conta.Domain.Repositories;

public interface ICarteiraEscritaRepository
{
    Task CriarNovaCarteira(Carteira carteira);
    
    Task<bool> Commit();
}