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

    public async Task<bool> ValidarContaCorrente(Guid contaCorrenteId, Guid usuarioId)
    {
        var request = new ValidarContaCorrenteRequest()
            { ContaCorrenteId = contaCorrenteId.ToString(), UsuarioId = usuarioId.ToString() };
        
        return (await _contaCorrenteProtoServiceClient.ValidarContaCorrenteAsync(request)).EhValido;
    }

    public async Task<bool> SomarSaldoContaCorrente(Guid usuarioId, Guid contaCorrenteId, decimal valor)
    {
        var request = new SomarSubtrairSaldoRequest()
        {
            UsuarioId = usuarioId.ToString(),
            ContaCorrenteId = contaCorrenteId.ToString(),
            Valor = (double) valor
        };
        
        return (await _contaCorrenteProtoServiceClient.SomarSaldoContaCorrenteAsync(request)).Sucesso;
    }

    public async Task<bool> SubtrairSaldoContaCorrente(Guid usuarioId, Guid contaCorrenteId, decimal valor)
    {
        var request = new SomarSubtrairSaldoRequest()
        {
            UsuarioId = usuarioId.ToString(),
            ContaCorrenteId = contaCorrenteId.ToString(),
            Valor = (double) valor
        };

        return (await _contaCorrenteProtoServiceClient.SubtrairSaldoContaCorrenteAsync(request)).Sucesso;
    }
}