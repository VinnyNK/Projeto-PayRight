using System;
using PayRight.Conta.Domain.Commands;
using PayRight.Conta.Tests.TestesUnitarios.Command.Fixtures;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Command;

[Collection(nameof(SaldoContaCorrenteCommandCollection))]
public class SubtrairSaldoContaCorrenteCommandTests
{
    private readonly SubtrairSaldoContaCorrenteCommandFixture _subtrairSaldoContaCorrenteCommandFixture;

    public SubtrairSaldoContaCorrenteCommandTests(SubtrairSaldoContaCorrenteCommandFixture subtrairSaldoContaCorrenteCommandFixture)
    {
        _subtrairSaldoContaCorrenteCommandFixture = subtrairSaldoContaCorrenteCommandFixture;
    }

    [Trait("Command", "SubtrairSaldo")]
    [Fact]
    public void DeveRetornarSucessoComandoValido()
    {
        // Arrange
        var command = _subtrairSaldoContaCorrenteCommandFixture.GerarCommand();
        
        // Act
        command.Validar();
        var resultado = command.IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("Command", "SubtrairSaldo")]
    [Fact]
    public void DeveRetornarErroGuidVazio()
    {
        // Arrange
        var command = _subtrairSaldoContaCorrenteCommandFixture.GerarCommand(Guid.Empty, Guid.Empty);
        
        // Act
        command.Validar();
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }

    [Trait("Command", "SubtrairSaldo")]
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-99999)]
    public void DeveRetornarErroValorInvalido(decimal valor)
    {
        // Arrange
        var command = _subtrairSaldoContaCorrenteCommandFixture.GerarCommand(valor: valor);
        
        // Act
        command.Validar();
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
}