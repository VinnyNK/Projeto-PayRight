using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PayRight.Conta.API.DTOs;
using PayRight.Conta.Domain.Commands;
using PayRight.Conta.Domain.Queries;
using PayRight.Shared.Controllers;
using PayRight.Shared.Mediator;

namespace PayRight.Conta.API.Controllers;

[Route("/api/Contas/ContasCorrentes")]
public class ContasCorrentesController : MainController
{
    private readonly IContaCorrenteQueries _contaCorrenteQueries;

    public ContasCorrentesController(IMapper mapper, IMediatorHandler mediatorHandler, IContaCorrenteQueries contaCorrenteQueries) : base(mapper, mediatorHandler)
    {
        _contaCorrenteQueries = contaCorrenteQueries;
    }

    [HttpGet]
    public async Task<IActionResult> BuscaContasCorrentes()
    {
        var usuarioId = BuscaIdDoUsuarioAutenticado();
        if (usuarioId == Guid.Empty) return Unauthorized();

        var contasCorrentes = await _contaCorrenteQueries.BuscarContasCorrentes((Guid) usuarioId);

        return Ok(contasCorrentes.Select(_ => Mapper?.Map<ListarContasCorrentesResponse>(_)));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> BuscarContaCorrente(Guid id)
    {
        var usuarioId = BuscaIdDoUsuarioAutenticado();
        if (usuarioId == Guid.Empty) return Unauthorized();

        var contaCorrente = await _contaCorrenteQueries.BuscarContaCorrente(usuarioId, id);

        if (contaCorrente == null)
            return NotFound();

        return Ok(Mapper!.Map<ContaCorrenteResponse>(contaCorrente));
    }

    [HttpPost]
    public async Task<IActionResult> CriarContaCorrente([FromBody] ContaCorrenteRequest contaCorrenteRequest)
    {
        contaCorrenteRequest.UsuarioId = BuscaIdDoUsuarioAutenticado();
        var command = Mapper!.Map<CriarNovaContaCorrenteCommand>(contaCorrenteRequest);
        if (!command.IsValid)
            return RetornaErro(command.Notifications);

        var resultado = await MediatorHandler!.EnviarComando(command);
        
        return !resultado.Sucesso ? RetornaErro(resultado.CommandNotifications) : Created(nameof(CriarContaCorrente), null);
    }
}