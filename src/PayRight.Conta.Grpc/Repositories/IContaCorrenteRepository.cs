namespace PayRight.Conta.Grpc.Repositories;

public interface IContaCorrenteRepository
{
    Task<bool> ExisteContaCorrente(Guid contaCorrenteId, Guid usuarioId);
}