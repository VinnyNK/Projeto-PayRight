using PayRight.Cadastro.Tests.TestesUnitarios.ValueObjects.Fixtures;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.ValueObjects;

[Collection(nameof(EmailCollection))]
public class EmailTests
{
    private readonly EmailFixture _emailFixture;

    public EmailTests(EmailFixture emailFixture)
    {
        _emailFixture = emailFixture;
    }

    [Trait("ValueObject ", "Email")]
    [Fact]
    public void DeveRetornarSucessoNovoEmailValido()
    {
        // Arrange
        var email = _emailFixture.GerarEmail();

        // Act
        var resultado = email.IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("ValueObject ", "Email")]
    [Theory]
    [InlineData("fulano.gmail.com")]
    [InlineData("email@example.")]
    [InlineData("joao@gmailcom")]
    [InlineData("maria@hotma")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroNovoEmailEnderecoInvalido(string enderenco)
    {
        // Arrange
        var email = _emailFixture.GerarEmail(enderenco);

        // Act
        var resultado = email.IsValid;

        // Assert
        Assert.False(resultado);
    }
}