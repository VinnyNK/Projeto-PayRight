using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PayRight.Extrato.API.DTOs;
using PayRight.Extrato.Domain.Commands;
using PayRight.Shared.Controllers;
using PayRight.Shared.Mediator;

namespace PayRight.Extrato.API.Controllers;

[Route("api/Extratos/ContaCorrente/{contaCorrenteId:Guid}/Atividades")]
public class AtividadesController : MainController
{
    public AtividadesController(IMapper mapper, IMediatorHandler mediatorHandler) : base(mapper, mediatorHandler)
    { }
    
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
}