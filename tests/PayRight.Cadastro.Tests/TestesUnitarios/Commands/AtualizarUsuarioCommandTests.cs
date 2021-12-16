using PayRight.Cadastro.Tests.TestesUnitarios.Commands.Fixtures;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.Commands;

[Collection(nameof(AtualizarUsuarioCommandCollection))]
public class AtualizarUsuarioCommandTests
{
    private readonly AtualizarUsuarioCommandFixture _atualizarUsuarioCommandFixture;

    public AtualizarUsuarioCommandTests(AtualizarUsuarioCommandFixture atualizarUsuarioCommandFixture)
    {
        _atualizarUsuarioCommandFixture = atualizarUsuarioCommandFixture;
    }

    [Trait("Command", "AtualizarUsuario")]
    [Fact]
    public void DeveRetornarSucessoAtualizarUsuarioCommandValido()
    {
        // Arrange
        var command = _atualizarUsuarioCommandFixture.GerarAtualizarUsuarioCommand();
        
        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("Command", "AtualizarUsuario")]
    [Theory]
    [InlineData("email@example")]
    [InlineData("email@example.")]
    [InlineData("email@examplecom")]
    [InlineData("emailexample.com")]
    [InlineData("@example.com")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroAtualizarUsuarioCommandEnderecoEmailInvalido(string enderecoEmail)
    {
        // Arrange
        var command = _atualizarUsuarioCommandFixture.GerarAtualizarUsuarioCommand(enderecoEmail: enderecoEmail);
        
        // Act
        var resultado = command.IsValid;

        //Assert
        Assert.False(resultado);
    }

    [Trait("Command", "AtualizarUsuario")]
    [Theory]
    [InlineData("an")]
    [InlineData("Fulano ")]
    [InlineData(" Fulano")]
    [InlineData("Ful ano")]
    [InlineData("vHQbuuHmjkMjekcnHKosdxUtAANkwNxtNFgeYeLsRCYdFUVHqodvNArtsxPgFEXMa")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroAtualizarUsuarioCommandPrimeiroNomeInvalido(string primeiroNome)
    {
        // Arrange
        var command = _atualizarUsuarioCommandFixture.GerarAtualizarUsuarioCommand(primeiroNome);

        // Act
        var resultado = command.IsValid;

        //Assert
        Assert.False(resultado);
    }
    
    [Trait("Command", "AtualizarUsuario")]
    [Theory]
    [InlineData("an")]
    [InlineData("vHQbuuHmjkMjekcnHKosdxUtAANkwNxtNFgeYeLsRCYdFUVHqodvNArtsxPgFEXMa")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroAtualizarUsuarioCommandSobrenomeInvalido(string sobrenome)
    {
        // Arrange
        var command = _atualizarUsuarioCommandFixture.GerarAtualizarUsuarioCommand(sobrenome: sobrenome);

        // Act
        var resultado = command.IsValid;

        //Assert
        Assert.False(resultado);
    }
}