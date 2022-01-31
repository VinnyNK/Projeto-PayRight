using PayRight.Extrato.Domain.Entities;

namespace PayRight.Extrato.Domain.Repositories;

public interface IAtividadeLeituraRepository
{
    Task<Atividade?> BuscarAtividade(Guid contaCorrenteId, Guid atividadeId);
}