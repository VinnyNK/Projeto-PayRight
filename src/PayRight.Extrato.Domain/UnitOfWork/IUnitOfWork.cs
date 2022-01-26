using PayRight.Extrato.Domain.Repositories;

namespace PayRight.Extrato.Domain.UnitOfWork;

public interface IUnitOfWork
{
    IContaCorrenteExtratoLeituraRepository ContaCorrenteExtratoLeituraRepository { get; }
    IContaCorrenteExtratoEscritaRepository ContaCorrenteExtratoEscritaRepository { get; }
    IAtividadeLeituraRepository AtividadeLeituraRepository { get; }
    IAtividadeEscritaRepository AtividadeEscritaRepository { get; }
    
    Task<bool> Commit();
}