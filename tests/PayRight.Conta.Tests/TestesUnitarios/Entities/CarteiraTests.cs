using System;
using PayRight.Conta.Tests.TestesUnitarios.Entities.Fixtures;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Entities;

[Collection(nameof(CarteiraCollection))]
public class CarteiraTests
{
    private readonly CarteiraFixture _carteiraFixture;

    public CarteiraTests(CarteiraFixture carteiraFixture)
    {
        _carteiraFixture = carteiraFixture;
    }

    [Trait("Entity", "Carteira")]
    [Fact]
    public void DeveRetornarSucessoNovaCarteiraValida()
    {
        // Arrange
        var carteira = _carteiraFixture.GerarNovaCarteira();

        // Act
        var resultado = carteira.IsValid;

        // Assert
        Assert.True(resultado);
        Assert.True(carteira.Ativo);
        Assert.IsType<Guid>(carteira.UsuarioId);
        Assert.Equal(0, carteira.Saldo);
    }
    
    [Trait("Entity", "Carteira")]
    [Theory]
    [InlineData(200, "100", "50", "50")]
    [InlineData(333, "100", "233")]
    [InlineData(502.2, "125", "377,2")]
    [InlineData(100.01, "100", "0,01")]
    public void DeveRetornarValorEsperadoSomarSaldo(decimal valorEsperado, params string[] valorSoma)
    {
        // Arrange
        var carteira = _carteiraFixture.GerarNovaCarteira();
        foreach (var valor in valorSoma)
            carteira.SomarSaldo(decimal.Parse(valor));

        // Act
        var resultado = carteira.Saldo;

        // Assert
        Assert.Equal(valorEsperado, resultado);
    }
    
    [Trait("Entity", "Carteira")]
    [Theory]
    [InlineData(0, 200, "100", "50", "50")]
    [InlineData(133, 466, "233", "100")]
    [InlineData(252.2, 754.4, "377,2", "125")]
    [InlineData(99.99, 200, "100", "0,01")]
    public void DeveRetornarValorEsperadoSubtrairSaldo(decimal valorEsperado, decimal saldoInicial, params string[] valorSoma)
    {
        // Arrange
        var contaCorrente = _carteiraFixture.GerarNovaCarteira();
        contaCorrente.SomarSaldo(saldoInicial);
        foreach (var valor in valorSoma)
            contaCorrente.SubtrairSaldo(decimal.Parse(valor));

        // Act
        var resultado = contaCorrente.Saldo;

        // Assert
        Assert.Equal(valorEsperado, resultado);
    }

    [Trait("Entity", "Carteira")]
    [Theory]
    [InlineData(100, "100", "50", "50")]
    [InlineData(50, "233", "100")]
    [InlineData(500.4, "377,2", "125")]
    [InlineData(10, "100", "0,01")]
    public void DeveRetornarErroSaldoNegativoCarteira(decimal saldoInicial, params string[] valores)
    {
        // Arrange
        var contaCorrente = _carteiraFixture.GerarNovaCarteira();
        contaCorrente.SomarSaldo(saldoInicial);
        foreach (var valor in valores)
            contaCorrente.SubtrairSaldo(decimal.Parse(valor));

        // Act
        var resultado = contaCorrente.IsValid;

        // Assert
        Assert.False(resultado);
    }
}
