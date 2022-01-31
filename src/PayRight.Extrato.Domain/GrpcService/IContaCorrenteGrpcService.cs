namespace PayRight.Extrato.Domain.GrpcService;

public interface IContaCorrenteGrpcService
{
    Task<bool> ValidarContaCorrente(Guid contaCorrenteId, Guid usuarioId);

    Task<bool> SomarSaldoContaCorrente(Guid usuarioId, Guid contaCorrenteId, decimal valor);

    Task<bool> SubtrairSaldoContaCorrente(Guid usuarioId, Guid contaCorrenteId, decimal valor);
}