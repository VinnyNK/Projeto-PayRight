using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PayRight.Cadastro.API.DTOs;
using PayRight.Cadastro.Domain.Commands;
using PayRight.Cadastro.Domain.Queries;
using PayRight.Shared.Controllers;
using PayRight.Shared.Mediator;

namespace PayRight.Cadastro.API.Controllers;

[Route("api/[controller]")]
public class UsuariosController : MainController
{
    private readonly IUsuarioQueries _usuarioQueries;
    
    public UsuariosController(IMapper mapper, 
        IMediatorHandler mediatorHandler, IUsuarioQueries usuarioQueries) : base(mapper, mediatorHandler)
    {
        _usuarioQueries = usuarioQueries;
    }

    [HttpGet("{usuarioId:guid}")]
    public async Task<IActionResult> BuscaInformacaoUsuario(Guid usuarioId)
    {
        var usuario = await _usuarioQueries.BuscaInfoUsuario(usuarioId);

        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }
    
    [HttpGet("{usuarioId:guid}/completo")]
    public async Task<IActionResult> BuscaInformacaoUsuarioCompleto(Guid usuarioId)
    {
        var usuario = await _usuarioQueries.BuscaUsuarioCompleto(usuarioId);

        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> CriarUsuario(CriarNovoUsuarioRequestDto criarNovoUsuarioRequestDto)
    {
        var criarNovoUsuarioCommand = Mapper.Map<CriarNovoUsuarioCpfCommand>(criarNovoUsuarioRequestDto);

        if (!criarNovoUsuarioCommand.IsValid)
            return RetornaErro(criarNovoUsuarioCommand.Notifications);
        
        var resultado = await MediatorHandler.EnviarComando(criarNovoUsuarioCommand);

        return !resultado.Sucesso ? RetornaErro(resultado.CommandNotifications) : Created(nameof(CriarUsuario), null);
    }

    [HttpPut("{usuarioId:guid}")]
    public async Task<IActionResult> AtualizarUsuario(AtualizarUsuarioRequestDto atualizarUsuarioRequestDto, Guid usuarioId)
    {
        var atualizarUsuarioCommand = Mapper.Map<AtualizarUsuarioCommand>(atualizarUsuarioRequestDto);
        atualizarUsuarioCommand.Id = usuarioId;
        
        if (!atualizarUsuarioCommand.IsValid)
            return RetornaErro(atualizarUsuarioCommand.Notifications);

        var resultado = await MediatorHandler.EnviarComando(atualizarUsuarioCommand);
        
        return !resultado.Sucesso ? RetornaErro(resultado.CommandNotifications) : NoContent(); 
    }
}
