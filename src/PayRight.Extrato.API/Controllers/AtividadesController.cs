using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PayRight.Extrato.API.DTOs;
using PayRight.Extrato.Domain.Commands;
using PayRight.Extrato.Domain.Queries;
using PayRight.Shared.Controllers;
using PayRight.Shared.Mediator;

namespace PayRight.Extrato.API.Controllers;

[Route("api/Extratos/ContaCorrente/{contaCorrenteId:Guid}/Atividades")]
public class AtividadesController : MainController
{
    private readonly IAtividadeQueries _atividadeQueries;
    
    public AtividadesController(IMapper mapper, IMediatorHandler mediatorHandler, IAtividadeQueries atividadeQueries) : base(mapper, mediatorHandler)
    {
        _atividadeQueries = atividadeQueries;
    }
    
    [HttpPost]
    public async Task<IActionResult> CriarAtividadeContaCorrente(AtividadeContaCorrenteRequestDTO atividadeContaCorrenteRequestDto, Guid contaCorrenteId)
    {
        atividadeContaCorrenteRequestDto.ContaCorrenteId = contaCorrenteId;
        atividadeContaCorrenteRequestDto.UsuarioId = BuscaIdDoUsuarioAutenticado();
        var command = Mapper!.Map<CriarAtividadeContaCorrenteCommand>(atividadeContaCorrenteRequestDto);
        if (!command.IsValid)
            return RetornaErro(command.Notifications);

        var resultado = await MediatorHandler?.EnviarComando(command)!;
        
        return !resultado.Sucesso ? RetornaErro(resultado.CommandNotifications) : Created(nameof(CriarAtividadeContaCorrente), null);
    }

    [HttpGet("{atividadeId:Guid}/Pagar")]
    public async Task<IActionResult> PagarAtividadeContaCorrente(Guid atividadeId, Guid contaCorrenteId, [FromQuery] DateTime? dataPagamento)
    {
        var command = new PagarAtividadeContaCorrenteCommand(BuscaIdDoUsuarioAutenticado(), contaCorrenteId,
            atividadeId, DateOnly.FromDateTime(dataPagamento ?? DateTime.Today));

        var resultado = await MediatorHandler?.EnviarComando(command)!;
        
        return !resultado.Sucesso ? RetornaErro(resultado.CommandNotifications) : NoContent();
    }

    [HttpGet("{atividadeId:Guid}")]
    public async Task<IActionResult> BuscarAtividadePorId(Guid atividadeId, Guid contaCorrenteId)
    {
        var atividade = await _atividadeQueries.BuscaAtividadePorId(atividadeId, BuscaIdDoUsuarioAutenticado(), contaCorrenteId);

        return atividade == null ? NotFound() : Ok(atividadeId);
    }
}