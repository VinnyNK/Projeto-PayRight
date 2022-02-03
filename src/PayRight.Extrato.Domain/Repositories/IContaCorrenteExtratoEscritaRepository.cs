using PayRight.Extrato.Domain.Entities;

namespace PayRight.Extrato.Domain.Repositories;

public interface IContaCorrenteExtratoEscritaRepository
{
    Task AdicionaExtrato(ContaCorrenteExtrato extrato);
    void AtualizarExtrato(ContaCorrenteExtrato extrato);
    Task AdicionaOuAtualizaExtrato(ContaCorrenteExtrato extrato);
    
}