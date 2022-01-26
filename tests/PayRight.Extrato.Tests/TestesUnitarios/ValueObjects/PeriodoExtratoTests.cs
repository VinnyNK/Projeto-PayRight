using System;
using PayRight.Extrato.Tests.TestesUnitarios.ValueObjects.Fixture;
using Xunit;

namespace PayRight.Extrato.Tests.TestesUnitarios.ValueObjects;

[Collection(nameof(PeriodoExtratoCollection))]
public class PeriodoExtratoTests
{
    private readonly PeriodoExtratoFixture _periodoExtratoFixture;

    public PeriodoExtratoTests(PeriodoExtratoFixture periodoExtratoFixture)
    {
        _periodoExtratoFixture = periodoExtratoFixture;
    }

    [Trait("ValueObject", "PeriodoExtrato")]
    [Fact]
    public void DeveRetornarSucessoPeriodoExtratoValido()
    {
        // Arrange
        var periodoExtrato = _periodoExtratoFixture.GerarPeriodoExtrato();
        
        // Act
        periodoExtrato.Validar();
        var resultado = periodoExtrato.IsValid;
        
        // Assert
        Assert.True(resultado);
    }

    [Trait("ValueObject", "PeriodoExtrato")]
    [Theory]
    [InlineData(13, 2022)]
    [InlineData(12, 2021)]
    [InlineData(0, null)]
    [InlineData(0, 2100)]
    public void DeveRetornarErroPeriodoExtratoInvalido(int mes, int? ano)
    {
        // Arrange
        var periodoExtrato = _periodoExtratoFixture.GerarPeriodoExtrato(mes, ano ?? DateTime.Now.Year);

        // Act
        periodoExtrato.Validar();
        var resultado = periodoExtrato.IsValid;

        // Assert
        Assert.False(resultado);
    }
}