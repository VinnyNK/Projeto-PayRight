using Grpc.Core;
using PayRight.Conta.Grpc.Protos;
using PayRight.Conta.Domain.Commands;
using PayRight.Conta.Domain.Queries;
using PayRight.Shared.Mediator;


namespace PayRight.Conta.Grpc.Services;

public class ContaCorrenteService : ContaCorrenteProtoService.ContaCorrenteProtoServiceBase
{
    private readonly IContaCorrenteQueries _contaCorrenteQueries;
    private readonly IMediatorHandler _mediatorHandler;

    public ContaCorrenteService(IMediatorHandler mediatorHandler, IContaCorrenteQueries contaCorrenteQueries)
    {
        _mediatorHandler = mediatorHandler;
        _contaCorrenteQueries = contaCorrenteQueries;
    }

    public override async Task<ValidarContaCorrenteResponse> ValidarContaCorrente(ValidarContaCorrenteRequest request, ServerCallContext context)
    {
        return new ValidarContaCorrenteResponse()
        {
            EhValido = await _contaCorrenteQueries.ValidaContaCorrenteComUsuario(Guid.Parse(request.ContaCorrenteId),
                Guid.Parse(request.UsuarioId))
        };
    }

    public override async Task<SomarSubtrairSaldoResponse> SomarSaldoContaCorrente(SomarSubtrairSaldoRequest request, ServerCallContext context)
    {
        var command = new SomaSaldoContaCorrenteCommand(Guid.Parse(request.UsuarioId), Guid.Parse(request.ContaCorrenteId), (decimal) request.Valor);
        var resultado = await _mediatorHandler.EnviarComando(command);

        return new SomarSubtrairSaldoResponse() { Sucesso = resultado.Sucesso };
    }

    public override async Task<SomarSubtrairSaldoResponse> SubtrairSaldoContaCorrente(SomarSubtrairSaldoRequest request, ServerCallContext context)
    {
        var command = new SubtrairSaldoContaCorrenteCommand(Guid.Parse(request.UsuarioId), Guid.Parse(request.ContaCorrenteId), (decimal) request.Valor);
        var resultado = await _mediatorHandler.EnviarComando(command);

        return new SomarSubtrairSaldoResponse() { Sucesso = resultado.Sucesso };
    }
}