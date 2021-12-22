using System;
using PayRight.Autenticacao.Tests.TestesUnitarios.ValueObjects.Fixtures;
using Xunit;

namespace PayRight.Autenticacao.Tests.TestesUnitarios.ValueObjects;

[Collection(nameof(TokenTestsCollection))]
public class TokenTests
{
    private readonly TokenTestsFixture _tokenTestsFixture;

    public TokenTests(TokenTestsFixture tokenTestsFixture)
    {
        _tokenTestsFixture = tokenTestsFixture;
    }

    [Trait("ValueObject", "Token")]
    [Fact]
    public void DeveRetornarSucessoTokenValido()
    {
        // Arrange
        var token = _tokenTestsFixture.GerarToken();

        // Act
        var resultado = token.IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("ValueObject", "Token")]
    [Theory]
    [InlineData("SzXo&#f5xbXJL!HxPb*!p^QLnAN&RCuwXFnS@Hi7LMuW4AFZ!SgdyZWEv%#o355pcN94")]
    [InlineData("naosouumtokenvalido")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroTokenValorInvalido(string valueToken)
    {
        // Arrange
        var token = _tokenTestsFixture.GerarToken(valueToken);

        // Act
        var resultado = token.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("ValueObject", "Token")]
    [Fact]
    public void DeveRetornarErroTokenValidoAteAnteriorCriado()
    {
        // Arrange
        var token = _tokenTestsFixture.GerarToken(validoAte: DateTime.Now.AddDays(-1));

        // Act
        var resultado = token.IsValid;

        // Assert
        Assert.False(resultado);
    }
}