using PayRight.Conta.Grpc.Protos;
using PayRight.Extrato.Domain.GrpcService;

namespace PayRight.Extrato.API.GrpcService;

public class ContaCorrenteGrpcService : IContaCorrenteGrpcService
{
    private readonly ContaCorrenteProtoService.ContaCorrenteProtoServiceClient _contaCorrenteProtoServiceClient;

    public ContaCorrenteGrpcService(ContaCorrenteProtoService.ContaCorrenteProtoServiceClient contaCorrenteProtoServiceClient)
    {
        _contaCorrenteProtoServiceClient = contaCorrenteProtoServiceClient;
    }

    public async Task<ValidarContaCorrenteResponse> ValidarContaCorrente(Guid contaCorrenteId, Guid usuarioId)
    {
        var request = new ValidarContaCorrenteRequest()
            { ContaCorrenteId = contaCorrenteId.ToString(), UsuarioId = usuarioId.ToString()};
        return await _contaCorrenteProtoServiceClient.ValidarContaCorrenteAsync(request);
    }
}