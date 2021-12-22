using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using PayRight.Autenticacao.API.Models;
using PayRight.Autenticacao.API.Repositories;
using PayRight.Autenticacao.API.Services;
using PayRight.Autenticacao.API.ValueObjects;
using Xunit;

namespace PayRight.Autenticacao.Tests.TestesUnitarios.Services;

public class AutenticacaoServiceTests
{
    [Trait("Service", "Autenticacao")]
    [Fact]
    public async Task DeveRetornarTokenQuandoInformacoesValidas()
    {
        // Arrange
        var usuario = new Mock<Usuario>("email@example.com", "123321");
        usuario.Setup(_ => _.ValidarSenha("123321")).Returns(true);
        var usuarioRepo = new Mock<IUsuarioAutenticacaoRepository>();
        usuarioRepo.Setup(_ => _.BuscaUsuarioPorEmail("email@example.com")).Returns(Task.FromResult<Usuario?>(usuario.Object));
        var inMemoryConfiguration = new Dictionary<string, string>
        {
            {"JWT:Secret", "y!qMzUux%SLLijkoa6bbgVY@%dW!eyT6ADv$x&hRfHva9@uK$snpGhWSh$7T8CeGRkk*"},
            {"JWT:ValidIssuer", "PayRightAuthenticatorTESTS"},
            {"JWT:ValidAudience", "https://localhost"},
            {"JWT:ExpiresIn", "1"}
        };
        
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemoryConfiguration)
            .Build();
        var service = new AutenticacaoService(usuarioRepo.Object, configuration);
        
        // Act
        var token = await service.Login("email@example.com", "123321")!;
        var resultado = service.IsValid;

        // Assert
        Assert.True(resultado);
        Assert.IsType<Token>(token);
    }
    
    
    [Trait("Service", "Autenticacao")]
    [Fact]
    public async Task DeveRetornarErroEmailNaoExiste()
    {
        // Arrange
        var usuarioRepo = new Mock<IUsuarioAutenticacaoRepository>();
        usuarioRepo.Setup(_ => _.BuscaUsuarioPorEmail("email@example.com")).Returns(Task.FromResult<Usuario?>(null));
        var configuration = new Mock<IConfiguration>();
        var service = new AutenticacaoService(usuarioRepo.Object, configuration.Object);
        
        // Act
        var token = await service.Login("email@example.com", "123321")!;
        var resultado = service.IsValid;

        // Assert
        Assert.False(resultado);
        Assert.Null(token);
    }

    [Trait("Service", "Autenticacao")]
    [Fact]
    public async Task DeveRetornarErroEmailEmBranco()
    {
        // Arrange
        var usuarioRepo = new Mock<IUsuarioAutenticacaoRepository>();
        var configuration = new Mock<IConfiguration>();
        var service = new AutenticacaoService(usuarioRepo.Object, configuration.Object);
        
        // Act
        var token = await service.Login("", "123321")!;
        var resultado = service.IsValid;

        // Assert
        Assert.False(resultado);
        Assert.Null(token);
    }
    
    [Trait("Service", "Autenticacao")]
    [Fact]
    public async Task DeveRetornarErroSenhaEmBranco()
    {
        // Arrange
        var usuarioRepo = new Mock<IUsuarioAutenticacaoRepository>();
        var configuration = new Mock<IConfiguration>();
        var service = new AutenticacaoService(usuarioRepo.Object, configuration.Object);
        
        // Act
        var token = await service.Login("email@example.com", "")!;
        var resultado = service.IsValid;

        // Assert
        Assert.False(resultado);
        Assert.Null(token);
    }
}