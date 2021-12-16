using PayRight.Cadastro.Tests.TestesUnitarios.Entities.Fixtures;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.Entities;

[Collection(nameof(UsuarioCollection))]
public class UsuarioTestes
{
    private readonly UsuarioTestsFixture _usuarioFixture;

    public UsuarioTestes(UsuarioTestsFixture usuarioFixture)
    {
        _usuarioFixture = usuarioFixture;
    }

    [Trait("Entity", "Usuario")]
    [Fact]
    public void DeveRetornarSucessoNovoUsuarioValido()
    {
        // Arrange
        var usuario = _usuarioFixture.GerarNovoUsuario();

        // Act
        var resultado = usuario.IsValid;

        // Assert
        Assert.True(resultado);
    }
    
    [Trait("Entity", "Usuario")]
    [Fact]
    public void DeveRetornarTrueValidacaoDaSenhaDeUmUsuarioValido()
    {
        // Arrange
        const string senha = "12QWaszx!";
        var usuario = _usuarioFixture.GerarNovoUsuario(senha: senha, confirmacaoSenha: senha);

        // Act
        var senhaValida = usuario.ValidarSenha(senha);

        // Assert
        Assert.True(senhaValida);
    }
    
    [Trait("Entity", "Usuario")]
    [Fact]
    public void DeveRetornarFalseValidacaoDaSenhaDeUmUsuarioValido()
    {
        // Arrange
        const string senha = "12QWaszx!";
        const string senhaDiferente = "12QWaszx!1";
        var usuario = _usuarioFixture.GerarNovoUsuario(senha: senha, confirmacaoSenha: senha);

        // Act
        var senhaValida = usuario.ValidarSenha(senhaDiferente);

        // Assert
        Assert.False(senhaValida);
    }

    [Trait("Entity", "Usuario")]
    [Fact]
    public void DeveRetornarTrueNovoUsuarioHabilitado()
    {
        // Arrange
        var usuario = _usuarioFixture.GerarNovoUsuario();

        // Act
        usuario.HabilitarUsuario();

        // Assert
        Assert.True(usuario.Ativo);
    }
    
    [Trait("Entity", "Usuario")]
    [Fact]
    public void DeveRetornarFalseNovoUsuarioDesabilitado()
    {
        // Arrange
        var usuario = _usuarioFixture.GerarNovoUsuario();

        // Act
        usuario.DesabilitarUsuario();

        // Assert
        Assert.False(usuario.Ativo);
    }

    [Trait("Entity", "Usuario")]
    [Theory]
    [InlineData("123456789")]
    [InlineData("qwaszxqw")]
    [InlineData("qwas@23zxqw")]
    [InlineData("AVCX!23MNSDHJ")]
    [InlineData("ABC123def")]
    [InlineData("12345")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroNovoUsuarioSenhaInvalida(string senha)
    {
        // Arrange
        var usuario = _usuarioFixture.GerarNovoUsuario(senha: senha, confirmacaoSenha: senha);

        // Act
        var resultado = usuario.IsValid;

        // Assert
        Assert.False(resultado);
    }
}