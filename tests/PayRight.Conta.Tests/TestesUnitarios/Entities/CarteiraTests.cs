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
    [InlineData(200.00, 100.00, 50.00, 50.00)]
    [InlineData(333.00, 100.00, 233.00)]
    [InlineData(502.2, 125.00, 377.2)]
    [InlineData(100.01, 100.00, 0.01)]
    public void DeveRetornarValorEsperadoSomarSaldo(decimal valorEsperado, params double[] valorSoma)
    {
        // Arrange
        var carteira = _carteiraFixture.GerarNovaCarteira();
        foreach (var valor in valorSoma)
            carteira.SomarSaldo((decimal) valor);

        // Act
        var resultado = carteira.Saldo;

        // Assert
        Assert.Equal(valorEsperado, resultado);
    }
    
    [Trait("Entity", "Carteira")]
    [Theory]
    [InlineData(0.00, 200.00, 100.00, 50.00, 50.00)]
    [InlineData(133.00, 466.00, 233.00, 100.00)]
    [InlineData(252.2, 754.4, 377.2, 125.00)]
    [InlineData(99.99, 200.00, 100.00, 0.01)]
    public void DeveRetornarValorEsperadoSubtrairSaldo(decimal valorEsperado, decimal saldoInicial, params double[] valorSoma)
    {
        // Arrange
        var contaCorrente = _carteiraFixture.GerarNovaCarteira();
        contaCorrente.SomarSaldo(saldoInicial);
        foreach (var valor in valorSoma)
            contaCorrente.SubtrairSaldo((decimal) valor);

        // Act
        var resultado = contaCorrente.Saldo;

        // Assert
        Assert.Equal(valorEsperado, resultado);
    }

    [Trait("Entity", "Carteira")]
    [Theory]
    [InlineData(100.00, 100.00, 50.00, 50.00)]
    [InlineData(50.00, 233.00, 100.00)]
    [InlineData(500.4, 377.2, 125.00)]
    [InlineData(10.00, 100.00, 0.01)]
    public void DeveRetornarErroSaldoNegativoCarteira(decimal saldoInicial, params double[] valores)
    {
        // Arrange
        var contaCorrente = _carteiraFixture.GerarNovaCarteira();
        contaCorrente.SomarSaldo(saldoInicial);
        foreach (var valor in valores)
            contaCorrente.SubtrairSaldo((decimal) valor);

        // Act
        var resultado = contaCorrente.IsValid;

        // Assert
        Assert.False(resultado);
    }
}
