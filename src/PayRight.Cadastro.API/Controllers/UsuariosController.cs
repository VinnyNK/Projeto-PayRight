using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    
    [HttpGet]
    public async Task<IActionResult> BuscaInformacaoUsuario()
    {
        var id = BuscaIdDoUsuarioAutenticado();
        if (id == null) { return Unauthorized(); }
        var usuario = await _usuarioQueries.BuscaInfoUsuario((Guid) id);

        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }
    
    [HttpGet("completo")]
    public async Task<IActionResult> BuscaInformacaoUsuarioCompleto()
    {
        var id = BuscaIdDoUsuarioAutenticado();
        if (id == null) { return Unauthorized(); }
        var usuario = await _usuarioQueries.BuscaUsuarioCompleto((Guid) id);

        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CriarUsuario(CriarNovoUsuarioRequestDto criarNovoUsuarioRequestDto)
    {
        var criarNovoUsuarioCommand = Mapper!.Map<CriarNovoUsuarioCpfCommand>(criarNovoUsuarioRequestDto);

        if (!criarNovoUsuarioCommand.IsValid)
            return RetornaErro(criarNovoUsuarioCommand.Notifications);
        
        var resultado = await MediatorHandler!.EnviarComando(criarNovoUsuarioCommand);

        return !resultado.Sucesso ? RetornaErro(resultado.CommandNotifications) : Created(nameof(CriarUsuario), null);
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarUsuario(AtualizarUsuarioRequestDto atualizarUsuarioRequestDto)
    {
        var id = BuscaIdDoUsuarioAutenticado();
        if (id == null) { return Unauthorized(); }
        var atualizarUsuarioCommand = Mapper!.Map<AtualizarUsuarioCommand>(atualizarUsuarioRequestDto);
        atualizarUsuarioCommand.Id = (Guid) id;
        
        if (!atualizarUsuarioCommand.IsValid)
            return RetornaErro(atualizarUsuarioCommand.Notifications);

        var resultado = await MediatorHandler!.EnviarComando(atualizarUsuarioCommand);
        
        return !resultado.Sucesso ? RetornaErro(resultado.CommandNotifications) : NoContent(); 
    }
}
