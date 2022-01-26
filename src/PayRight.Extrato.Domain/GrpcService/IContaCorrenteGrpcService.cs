using PayRight.Conta.Grpc.Protos;

namespace PayRight.Extrato.Domain.GrpcService;

public interface IContaCorrenteGrpcService
{
    Task<ValidarContaCorrenteResponse> ValidarContaCorrente(Guid contaCorrenteId, Guid usuarioId);
}