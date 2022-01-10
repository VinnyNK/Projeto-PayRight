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
    [InlineData(200.00, 100.00, 50.00, 50.00)]
    [InlineData(333.00, 100.00, 233.00)]
    [InlineData(502.2, 125.00, 377.2)]
    [InlineData(100.01, 100.00, 0.01)]
    public void DeveRetornarValorEsperadoSomarSaldo(decimal valorEsperado, params double[] valorSoma)
    {
        // Arrange
        var contaCorrente = _contaCorrenteFixture.GerarNovoContaCorrente();
        foreach (var valor in valorSoma)
            contaCorrente.SomarSaldo((decimal) valor);

        // Act
        var resultado = contaCorrente.Saldo;

        // Assert
        Assert.Equal(valorEsperado, resultado);
    }
    
    [Trait("Entity", "ContaCorrente")]
    [Theory]
    [InlineData(0, 200.00, 100.00, 50.00, 50.00)]
    [InlineData(133, 466.00, 233.00, 100.00)]
    [InlineData(252.2, 754.4, 377.2, 125.00)]
    [InlineData(99.99, 200.00, 100.00, 0.01)]
    [InlineData(-30, 60.00, 30.00, 60.00)]
    public void DeveRetornarValorEsperadoSubtrairSaldo(decimal valorEsperado, decimal saldoInicial, params double[] valorSoma)
    {
        // Arrange
        var contaCorrente = _contaCorrenteFixture.GerarNovoContaCorrente();
        contaCorrente.SomarSaldo(saldoInicial);
        foreach (var valor in valorSoma)
            contaCorrente.SubtrairSaldo((decimal) valor);

        // Act
        var resultado = contaCorrente.Saldo;

        // Assert
        Assert.Equal(valorEsperado, resultado);
    }
}