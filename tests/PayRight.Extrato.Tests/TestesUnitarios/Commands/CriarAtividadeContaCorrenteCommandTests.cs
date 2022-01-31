using System;
using PayRight.Extrato.Tests.TestesUnitarios.Commands.Fixtures;
using Xunit;

namespace PayRight.Extrato.Tests.TestesUnitarios.Commands;

[Collection(nameof(CriarAtividadeContaCorrenteCommandCollection))]
public class CriarAtividadeContaCorrenteCommandTests
{
    private readonly CriarAtividadeContaCorrenteCommandFixture _fixture;

    public CriarAtividadeContaCorrenteCommandTests(CriarAtividadeContaCorrenteCommandFixture fixture)
    {
        _fixture = fixture;
    }

    [Trait("Command", "CriarAtividadeContaCorrente")]
    [Fact]
    public void DeveRetornarSucessoGerarCommandValido()
    {
        // Arrange
        var command = _fixture.GerarCommand();
        
        // Act
        command.Validar();
        var resultado = command.IsValid;

        // Assert
        Assert.True(resultado);
    }
    
    [Trait("Command", "CriarAtividadeContaCorrente")]
    [Fact]
    public void DeveRetornaErroGerarCommandContaCorrenteIdVazio()
    {
        // Arrange
        var command = _fixture.GerarCommand(Guid.Empty);
        
        // Act
        command.Validar();
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("Command", "CriarAtividadeContaCorrente")]
    [Fact]
    public void DeveRetornaErroGerarCommandUsuarioIdVazio()
    {
        // Arrange
        var command = _fixture.GerarCommand(usuarioId: Guid.Empty);
        
        // Act
        command.Validar();
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("Command", "CriarAtividadeContaCorrente")]
    [Fact]
    public void DeveRetornaErroGerarValorInvalido()
    {
        // Arrange
        var command = _fixture.GerarCommand(valor: -1);
        
        // Act
        command.Validar();
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("Command", "CriarAtividadeContaCorrente")]
    [Fact]
    public void DeveRetornaErroGerarDataEstimadaInvalida()
    {
        // Arrange
        var command = _fixture.GerarCommand(dataEstimado: DateOnly.FromDateTime(DateTime.Today).AddDays(-1));
        
        // Act
        command.Validar();
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
}