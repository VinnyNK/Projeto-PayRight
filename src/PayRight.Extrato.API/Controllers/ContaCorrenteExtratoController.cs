using Microsoft.AspNetCore.Mvc;
using PayRight.Extrato.Domain.Queries;
using PayRight.Shared.Controllers;

namespace PayRight.Extrato.API.Controllers;

[Route("api/Extratos")]
public class ContaCorrenteExtratoController : MainController
{
    private readonly IExtratoContaCorrenteQueries _extratoContaCorrenteQueries;

    public ContaCorrenteExtratoController(IExtratoContaCorrenteQueries extratoContaCorrenteQueries)
    {
        _extratoContaCorrenteQueries = extratoContaCorrenteQueries;
    } 
    
    [Route("{extratoId:Guid}/ContasCorrentes")]
    public async Task<IActionResult> BuscarExtrato(Guid extratoId)
    {
        var extrato = await _extratoContaCorrenteQueries.BuscarExtrato(extratoId, BuscaIdDoUsuarioAutenticado());

        return extrato == null ? NotFound() : Ok(extrato);
    }

    [Route("ContasCorrentes/{contaCorrenteId:Guid}")]
    public async Task<IActionResult> BuscarExtratoPorData(Guid contaCorrenteId, [FromQuery] int? mes, int? ano)
    {
        var extrato = await _extratoContaCorrenteQueries.BuscaExtratoPorData(contaCorrenteId, mes ?? DateTime.Today.Month, ano ?? DateTime.Today.Year);
        
        return extrato == null ? NotFound() : Ok(extrato);
    }
}