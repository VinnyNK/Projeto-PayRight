using PayRight.Cadastro.Tests.TestesUnitarios.ValueObjects.Fixtures;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.ValueObjects;

[Collection(nameof(NomeCompletoCollection))]
public class NomeCompletoTests
{
    private readonly NomeCompletoFixture _nomeCompletoFixture;

    public NomeCompletoTests(NomeCompletoFixture nomeCompletoFixture)
    {
        _nomeCompletoFixture = nomeCompletoFixture;
    }

    [Trait("ValueObject", "NomeCompleto")]
    [Fact]
    public void DeveRetornarSucessoNovoNomeCompletoValido()
    {
        // Arrange
        var nomeCompleto = _nomeCompletoFixture.GerarNomeCompleto();

        // Act
        var resultado = nomeCompleto.IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("ValueObject", "NomeCompleto")]
    [Theory]
    [InlineData("ana")]
    [InlineData("Leia")]
    [InlineData("vHQbuuHmjkMjekcnHKosdxUtAANkwNxtNFgeYeLsRCYdFUVHqodvNArtsxPgFEXM")]
    public void DeveRetornarSucessoNovoNomeCompletoPrimeiroNomeValido(string primeiroNome)
    {
        // Arrange
        var nomeCompleto = _nomeCompletoFixture.GerarNomeCompleto(primeiroNome);

        // Act
        var resultado = nomeCompleto.IsValid;

        // Assert
        Assert.True(resultado);
    }
    
    [Trait("ValueObject", "NomeCompleto")]
    [Theory]
    [InlineData("Han")]
    [InlineData("Solo")]
    [InlineData("vHQbuuHmjkMjekcnHKosdxUtAANkwNxtNFgeYeLsRCYdFUVHqodvNArtsxPgFEXM")]
    public void DeveRetornarSucessoNovoNomeCompletoSobrenomeValido(string sobrenome)
    {
        // Arrange
        var nomeCompleto = _nomeCompletoFixture.GerarNomeCompleto(sobrenome: sobrenome);

        // Act
        var resultado = nomeCompleto.IsValid;

        // Assert
        Assert.True(resultado);
    }
    
    [Trait("ValueObject", "NomeCompleto")]
    [Theory]
    [InlineData("an")]
    [InlineData("Fulano ")]
    [InlineData(" Fulano")]
    [InlineData("Ful ano")]
    [InlineData("vHQbuuHmjkMjekcnHKosdxUtAANkwNxtNFgeYeLsRCYdFUVHqodvNArtsxPgFEXMa")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void DeveRetornarErroNovoNomeCompletoPrimeiroNomeInvalido(string primeiroNome)
    {
        // Arrange
        var nomeCompleto = _nomeCompletoFixture.GerarNomeCompleto(primeiroNome);

        // Act
        var resultado = nomeCompleto.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("ValueObject", "NomeCompleto")]
    [Theory]
    [InlineData("an")]
    [InlineData(" ")]
    [InlineData("vHQbuuHmjkMjekcnHKosdxUtAANkwNxtNFgeYeLsRCYdFUVHqodvNArtsxPgFEXMa")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroNovoNomeCompletoSobrenomeInvalido(string sobrenome)
    {
        // Arrange
        var nomeCompleto = _nomeCompletoFixture.GerarNomeCompleto(sobrenome: sobrenome);

        // Act
        var resultado = nomeCompleto.IsValid;

        // Assert
        Assert.False(resultado);
    }
}