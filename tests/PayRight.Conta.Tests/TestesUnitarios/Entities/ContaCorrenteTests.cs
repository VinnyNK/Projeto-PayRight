using System;
using PayRight.Conta.Tests.TestesUnitarios.Entities.Fixtures;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Entities;

[Collection(nameof(ContaCorrenteCollection))]
public class ContaCorrenteTests
{
    private readonly ContaCorrenteFixture _contaCorrenteFixture;
    
    public ContaCorrenteTests(ContaCorrenteFixture contaCorrenteFixture)
    {
        _contaCorrenteFixture = contaCorrenteFixture;
    }

    [Trait("Entity", "ContaCorrente")]
    [Fact]
    public void DeveRetornarSucessoContaCorrenteValido()
    {
        // Arrange
        var contaCorrente = _contaCorrenteFixture.GerarNovoContaCorrente();

        // Act
        var resultado = contaCorrente.IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("Entity", "ContaCorrente")]
    [Theory]
    [InlineData("MKMKiM4fpBbyJLegiJJxsu0BV")]
    [InlineData("Ax")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroContaCorrenteNomeInvalido(string nome)
    {
        // Arrange
        var contaCorrente = _contaCorrenteFixture.GerarNovoContaCorrente(nome: nome);
        
        // Act
        var resultado = contaCorrente.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("Entity", "ContaCorrente")]
    [Theory]
    [InlineData("8pFdPoxJiL8nsAdm")]
    [InlineData("8pFdPoxJiL8nsAdm8pFdPoxJiL8nsAdm")]
    public void DeveRetornarErroContaCorrenteApelidoInvalido(string apelido)
    {
        // Arrange
        var contaCorrente = _contaCorrenteFixture.GerarNovoContaCorrente(apelido: apelido);
        
        // Act
        var resultado = contaCorrente.IsValid;

        // Assert
        Assert.False(resultado);
    }

    [Trait("Entity", "ContaCorrente")]
    [Theory]
    [InlineData(200, "100", "50", "50")]
    [InlineData(333, "100", "233")]
    [InlineData(502.2, "125", "377,2")]
    [InlineData(100.01, "100", "0,01")]
    public void DeveRetornarValorEsperadoSomarSaldo(decimal valorEsperado, params string[] valorSoma)
    {
        // Arrange
        var contaCorrente = _contaCorrenteFixture.GerarNovoContaCorrente();
        foreach (var valor in valorSoma)
            contaCorrente.SomarSaldo(decimal.Parse(valor));

        // Act
        var resultado = contaCorrente.Saldo;

        // Assert
        Assert.Equal(valorEsperado, resultado);
    }
    
    [Trait("Entity", "ContaCorrente")]
    [Theory]
    [InlineData(0, 200, "100", "50", "50")]
    [InlineData(133, 466, "233", "100")]
    [InlineData(252.2, 754.4, "377,2", "125")]
    [InlineData(99.99, 200, "100", "0,01")]
    [InlineData(-30, 60, "30", "60")]
    public void DeveRetornarValorEsperadoSubtrairSaldo(decimal valorEsperado, decimal saldoInicial, params string[] valorSoma)
    {
        // Arrange
        var contaCorrente = _contaCorrenteFixture.GerarNovoContaCorrente();
        contaCorrente.SomarSaldo(saldoInicial);
        foreach (var valor in valorSoma)
            contaCorrente.SubtrairSaldo(decimal.Parse(valor));

        // Act
        var resultado = contaCorrente.Saldo;

        // Assert
        Assert.Equal(valorEsperado, resultado);
    }
}