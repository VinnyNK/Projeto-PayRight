using PayRight.Extrato.Domain.Queries.DTOs;

namespace PayRight.Extrato.Domain.Queries;

public interface IExtratoContaCorrenteQueries
{
    Task<ExtratoContaCorrenteDTO?> BuscarExtrato(Guid extratoId, Guid usuarioId);

    Task<ExtratoContaCorrenteDTO?> BuscaExtratoPorData(Guid contaCorrenteId, int mes, int ano);
}