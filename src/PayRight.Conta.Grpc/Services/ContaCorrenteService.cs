using Grpc.Core;
using PayRight.Conta.Grpc.Protos;
using PayRight.Conta.Grpc.Repositories;

namespace PayRight.Conta.Grpc.Services;

public class ContaCorrenteService : ContaCorrenteProtoService.ContaCorrenteProtoServiceBase
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;


    public ContaCorrenteService(IContaCorrenteRepository contaCorrenteRepository)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
    }

    public override async Task<ValidarContaCorrenteResponse> ValidarContaCorrente(ValidarContaCorrenteRequest request, ServerCallContext context)
    {
        return new ValidarContaCorrenteResponse()
        {
            EhValido = await _contaCorrenteRepository.ExisteContaCorrente(Guid.Parse(request.ContaCorrenteId), Guid.Parse(request.ContaCorrenteId))
        };
    }
}