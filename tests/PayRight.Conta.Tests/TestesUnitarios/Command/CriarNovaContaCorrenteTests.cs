using System;
using PayRight.Conta.Tests.TestesUnitarios.Command.Fixtures;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Command;

[Collection(nameof(CriarNovaContaCorrenteCommandCollection))]
public class CriarNovaContaCorrenteTests
{
    private readonly CriarNovaContaCorrenteCommandFixture _criarNovaContaCorrenteCommandFixture;

    public CriarNovaContaCorrenteTests(CriarNovaContaCorrenteCommandFixture criarNovaContaCorrenteCommandFixture)
    {
        _criarNovaContaCorrenteCommandFixture = criarNovaContaCorrenteCommandFixture;
    }

    [Trait("Command", "CriarNovaContaCorrente")]
    [Fact]
    public void DeveRetornarSucessoNovoCommandoValido()
    {
        // Arrange
        var command = _criarNovaContaCorrenteCommandFixture.GerarCommand();
        
        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("Command", "CriarNovaContaCorrente")]
    [Fact]
    public void DeveRetornarErroNovoComandoGuidInvalido()
    {
        // Arrange
        var command = _criarNovaContaCorrenteCommandFixture.GerarCommand(Guid.Empty);
        
        // Act
        var resultado = command.IsValid;
        
        // Assert
        Assert.False(resultado);
    }

    [Trait("Command", "CriarNovaContaCorrente")]
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroNovoComandoNomeInvalido(string nome)
    {
        // Arrange
        var command = _criarNovaContaCorrenteCommandFixture.GerarCommand(nome: nome);
        
        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
}