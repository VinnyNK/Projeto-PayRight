using PayRight.Extrato.Domain.Queries.DTOs;

namespace PayRight.Extrato.Domain.Queries;

public interface IAtividadeQueries
{
    Task<AtividadeDTO?> BuscaAtividadePorId(Guid atividadeId, Guid usuarioId, Guid contaCorrenteId);
}