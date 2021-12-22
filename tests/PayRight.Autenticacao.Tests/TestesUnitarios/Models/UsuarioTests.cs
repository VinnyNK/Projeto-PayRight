using PayRight.Autenticacao.Tests.TestesUnitarios.Models.Fixtures;
using Xunit;

namespace PayRight.Autenticacao.Tests.TestesUnitarios.Models;

[Collection(nameof(UsuarioCollection))]
public class UsuarioTestes
{
    private readonly UsuarioTestsFixture _usuarioFixture;

    public UsuarioTestes(UsuarioTestsFixture usuarioFixture)
    {
        _usuarioFixture = usuarioFixture;
    }

    [Trait("Model", "Usuario")]
    [Fact]
    public void DeveRetornarSucessoNovoUsuarioValido()
    {
        // Arrange
        var usuario = _usuarioFixture.GerarNovoUsuario();
        
        // Act
        var resultado = usuario.IsValid;
        
        //Assert
        Assert.True(resultado);
    }

    [Trait("Model", "Usuario")]
    [Theory]
    [InlineData("fulano.gmail.com")]
    [InlineData("email@example.")]
    [InlineData("joao@gmailcom")]
    [InlineData("maria@hotma")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroNovoUsuarioEmailInvalido(string email)
    {
        // Arrange
        var usuario = _usuarioFixture.GerarNovoUsuario(email);

        // Act
        var resultado = usuario.IsValid;

        // Assert
        Assert.False(resultado);
    }

    [Trait("Model", "Usuario")]
    [Fact]
    public void DeveRetornarErroNovoUsuarioSenhaEmBranco()
    {
        // Arrange
        var usuario = _usuarioFixture.GerarNovoUsuario(senha: string.Empty);

        // Act
        var resultado = usuario.IsValid;

        // Asser
        Assert.False(resultado);
    }
}