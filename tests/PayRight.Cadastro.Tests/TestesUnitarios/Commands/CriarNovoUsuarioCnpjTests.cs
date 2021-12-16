using PayRight.Cadastro.Tests.TestesUnitarios.Commands.Fixtures;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.Commands;

[Collection(nameof(CriarNovoUsuarioCommandCollection))]
public class CriarNovoUsuarioCnpjTests
{
    private readonly CriarNovoUsuarioCommandFixture _criarNovoUsuarioCommandFixture;

    public CriarNovoUsuarioCnpjTests(CriarNovoUsuarioCommandFixture criarNovoUsuarioCommandFixture)
    {
        _criarNovoUsuarioCommandFixture = criarNovoUsuarioCommandFixture;
    }

    [Trait("Command", "CriarNovoUsuarioCNPJ")]
    [Fact]
    public void DeveRetornarSucessoCriarNovoUsuarioCommandValido()
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCnpjCommand();

        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("Command", "CriarNovoUsuarioCNPJ")]
    [Theory]
    [InlineData("an")]
    [InlineData("Fulano ")]
    [InlineData(" Fulano")]
    [InlineData("Ful ano")]
    [InlineData("vHQbuuHmjkMjekcnHKosdxUtAANkwNxtNFgeYeLsRCYdFUVHqodvNArtsxPgFEXMa")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void DeveRetornarErroCriarNovoUsuarioCommandPrimeiroNomeInvalido(string primeiroNome)
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCnpjCommand(primeiroNome);

        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("Command", "CriarNovoUsuarioCNPJ")]
    [Theory]
    [InlineData("an")]
    [InlineData(" ")]
    [InlineData("vHQbuuHmjkMjekcnHKosdxUtAANkwNxtNFgeYeLsRCYdFUVHqodvNArtsxPgFEXMa")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroCriarNovoUsuarioCommandSobrenomeInvalido(string sobrenome)
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCnpjCommand(sobrenome: sobrenome);

        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("Command", "CriarNovoUsuarioCNPJ")]
    [Theory]
    [InlineData("fulano.gmail.com")]
    [InlineData("email@example.")]
    [InlineData("joao@gmailcom")]
    [InlineData("maria@hotma")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroCriarNovoUsuarioCommandEnderecoEmailInvalido(string enderecoEmail)
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCnpjCommand(enderecoEmail: enderecoEmail);

        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("Command", "CriarNovoUsuarioCNPJ")]
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroCriarNovoUsuarioCommandNumeroDocumentoInvalido(string numeroDocumento)
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCnpjCommand(numeroDocumento: numeroDocumento);

        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("Command", "CriarNovoUsuarioCNPJ")]
    [Theory]
    [InlineData("12345")]
    [InlineData("MeuZ9")]
    [InlineData("YyDcTrQ7rdNIE8BuZjr07SOj1fXtgsIpfe9vZqj9bI6KLU6CGtFuwpa3RZwEAS1FWalzE7DbMDglBVwKr5i4ml480bMe7VXifBOXEs7bywBvy9B3Cb4CekieVMA1QO8w1")]
    [InlineData(null)]
    public void DeveRetornarErroCriarNovoUsuarioCommandSenhaInvalido(string senha)
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCnpjCommand(senha: senha);

        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
}