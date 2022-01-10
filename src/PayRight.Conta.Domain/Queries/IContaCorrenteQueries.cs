using PayRight.Conta.Domain.Queries.DTOs;

namespace PayRight.Conta.Domain.Queries;

public interface IContaCorrenteQueries
{
    Task<IEnumerable<ContaCorrenteDTO>> BuscarContasCorrentes(Guid usuarioId);
}