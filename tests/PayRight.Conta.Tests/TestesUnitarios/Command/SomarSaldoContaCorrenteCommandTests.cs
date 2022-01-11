using System;
using PayRight.Conta.Tests.TestesUnitarios.Command.Fixtures;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Command;

[Collection(nameof(SaldoContaCorrenteCommandCollection))]
public class SomarSaldoContaCorrenteCommandTests
{
    private readonly SomarSaldoContaCorrenteCommandFixture _somarSaldoContaCorrenteCommand;

    public SomarSaldoContaCorrenteCommandTests(SomarSaldoContaCorrenteCommandFixture subtrairSaldoContaCorrenteCommandFixture)
    {
        _somarSaldoContaCorrenteCommand = subtrairSaldoContaCorrenteCommandFixture;
    }

    [Trait("Command", "SomarSaldo")]
    [Fact]
    public void DeveRetornarSucessoComandoValido()
    {
        // Arrange
        var command = _somarSaldoContaCorrenteCommand.GerarCommand();
        
        // Act
        command.Validar();
        var resultado = command.IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("Command", "SomarSaldo")]
    [Fact]
    public void DeveRetornarErroGuidVazio()
    {
        // Arrange
        var command = _somarSaldoContaCorrenteCommand.GerarCommand(Guid.Empty, Guid.Empty);
        
        // Act
        command.Validar();
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }

    [Trait("Command", "SomarSaldo")]
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-99999)]
    public void DeveRetornarErroValorInvalido(decimal valor)
    {
        // Arrange
        var command = _somarSaldoContaCorrenteCommand.GerarCommand(valor: valor);
        
        // Act
        command.Validar();
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
}