using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayRight.Autenticacao.API.DTOs;
using PayRight.Autenticacao.API.Services;
using PayRight.Shared.Controllers;

namespace PayRight.Autenticacao.API.Controllers;

[Route("api/auth")]
public class AutenticacaoController : MainController
{
    private readonly IAutenticacaoService _autenticacaoService;
    
    public AutenticacaoController(IAutenticacaoService autenticacaoService, IMapper mapper) : base(mapper)
    {
        _autenticacaoService = autenticacaoService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var token = await _autenticacaoService.Login(loginDto.Email, loginDto.Senha)!;

        return token == null ? RetornaErro(_autenticacaoService.ServiceNotifications) : Ok(Mapper!.Map<TokenResponseDto>(token));
    }
}
