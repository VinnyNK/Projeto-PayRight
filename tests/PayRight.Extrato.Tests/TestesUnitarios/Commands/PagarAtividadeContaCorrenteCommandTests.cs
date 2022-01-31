using System;
using PayRight.Extrato.Tests.TestesUnitarios.Commands.Fixtures;
using Xunit;

namespace PayRight.Extrato.Tests.TestesUnitarios.Commands;

[Collection(nameof(PagarAtividadeContaCorrenteCommandCollection))]
public class PagarAtividadeContaCorrenteCommandTests
{
    private readonly PagarAtividadeContaCorrenteCommandFixture _fixture;

    public PagarAtividadeContaCorrenteCommandTests(PagarAtividadeContaCorrenteCommandFixture fixture)
    {
        _fixture = fixture;
    }

    [Trait("Command", "PagarAtividadeContaCorrente")]
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

    [Trait("Command", "PagarAtividadeContaCorrente")]
    [Fact]
    public void DeveRetornarErroGerarCommandUsuarioVazio()
    {
        // Arrange
        var command = _fixture.GerarCommand(Guid.Empty);
        
        // Act
        command.Validar();
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("Command", "PagarAtividadeContaCorrente")]
    [Fact]
    public void DeveRetornarErroGerarCommandContaCorrenteIdVazio()
    {
        // Arrange
        var command = _fixture.GerarCommand(contaCorrenteId: Guid.Empty);
        
        // Act
        command.Validar();
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("Command", "PagarAtividadeContaCorrente")]
    [Fact]
    public void DeveRetornarErroGerarCommandAtividadeIdVazio()
    {
        // Arrange
        var command = _fixture.GerarCommand(atividadeId: Guid.Empty);
        
        // Act
        command.Validar();
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
}