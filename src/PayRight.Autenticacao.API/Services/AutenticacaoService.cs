using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Flunt.Notifications;
using Microsoft.IdentityModel.Tokens;
using PayRight.Autenticacao.API.Models;
using PayRight.Autenticacao.API.Repositories;
using PayRight.Autenticacao.API.ValueObjects;
using PayRight.Shared.Services;

namespace PayRight.Autenticacao.API.Services;

public class AutenticacaoService : ServiceBase, IAutenticacaoService
{
    private readonly IUsuarioAutenticacaoRepository _usuarioAutenticacaoRepository;
    private readonly IConfiguration _configuration;

    public AutenticacaoService(IUsuarioAutenticacaoRepository usuarioAutenticacaoRepository, IConfiguration configuration)
    {
        _usuarioAutenticacaoRepository = usuarioAutenticacaoRepository;
        _configuration = configuration;
    }

    public async Task<Token?>? Login(string email, string senha)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
        {
            AddNotification("Credenciais", "Email e senha devem ser informados");
            return null;
        }

        var usuario = await _usuarioAutenticacaoRepository.BuscaUsuarioPorEmail(email);

        if (usuario != null)
        {
            var resultado = usuario.ValidarSenha(senha);
            if (resultado) return GerarToken(usuario);
        }
        
        AddNotification("Credenciais", "Usuario e/ou senha estao incorretos");
        return null;
    }

    private Token GerarToken(Usuario usuario)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString())
            }),
            Issuer = _configuration["JWT:ValidIssuer"],
            Audience = _configuration["JWT:ValidAudience"],
            Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration["JWT:ExpiresIn"])),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        };

        var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);

        return new Token(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
    }
}
