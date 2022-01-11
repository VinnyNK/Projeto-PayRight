using System;
using PayRight.Conta.Tests.TestesUnitarios.Messages.Fixtures;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Messages;

[Collection(nameof(UsuarioMessageCollection))]
public class UsuarioMessageTests
{
    private readonly UsuarioMessageFixture _usuarioMessageFixture;

    public UsuarioMessageTests(UsuarioMessageFixture usuarioMessageFixture)
    {
        _usuarioMessageFixture = usuarioMessageFixture;
    }

    [Trait("Message", "Usuario")]
    [Fact]
    public void DeveRetornarSucessoUsuarioMessageValido()
    {
        // Arrange
        var message = _usuarioMessageFixture.GerarMensagem();

        // Act
        message.Validar();
        var resultado = message.IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("Message", "Usuario")]
    [Fact]
    public void DeveRetornarErroUsuarioMessageAggregateIdInvalido()
    {
        // Arrange
        var message = _usuarioMessageFixture.GerarMensagem(Guid.Empty);

        // Act
        message.Validar();
        var resultado = message.IsValid;

        // Assert
        Assert.False(resultado);
    }
}