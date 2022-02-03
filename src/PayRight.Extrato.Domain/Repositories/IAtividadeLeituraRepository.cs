using PayRight.Extrato.Domain.Entities;

namespace PayRight.Extrato.Domain.Repositories;

public interface IAtividadeLeituraRepository
{
    Task<Atividade?> BuscarAtividadeComExtrato(Guid contaCorrenteId, Guid atividadeId);
    Task<Atividade?> BuscarAtividadePorId(Guid atividadeId, Guid usuarioId, Guid contaCorrenteId);
}