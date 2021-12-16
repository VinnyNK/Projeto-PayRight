using PayRight.Cadastro.Tests.TestesUnitarios.Commands.Fixtures;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.Commands;

[Collection(nameof(CriarNovoUsuarioCommandCollection))]
public class CriarNovoUsuarioCpfTests
{
    private readonly CriarNovoUsuarioCommandFixture _criarNovoUsuarioCommandFixture;

    public CriarNovoUsuarioCpfTests(CriarNovoUsuarioCommandFixture criarNovoUsuarioCommandFixture)
    {
        _criarNovoUsuarioCommandFixture = criarNovoUsuarioCommandFixture;
    }

    [Trait("Command", "CriarNovoUsuarioCPF")]
    [Fact]
    public void DeveRetornarSucessoCriarNovoUsuarioCommandValido()
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCpfCommand();

        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("Command", "CriarNovoUsuarioCPF")]
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
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCpfCommand(primeiroNome);

        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("Command", "CriarNovoUsuarioCPF")]
    [Theory]
    [InlineData("an")]
    [InlineData(" ")]
    [InlineData("vHQbuuHmjkMjekcnHKosdxUtAANkwNxtNFgeYeLsRCYdFUVHqodvNArtsxPgFEXMa")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroCriarNovoUsuarioCommandSobrenomeInvalido(string sobrenome)
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCpfCommand(sobrenome: sobrenome);

        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("Command", "CriarNovoUsuarioCPF")]
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
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCpfCommand(enderecoEmail: enderecoEmail);

        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("Command", "CriarNovoUsuarioCPF")]
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroCriarNovoUsuarioCommandNumeroDocumentoInvalido(string numeroDocumento)
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCpfCommand(numeroDocumento: numeroDocumento);

        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("Command", "CriarNovoUsuarioCPF")]
    [Theory]
    [InlineData("12345")]
    [InlineData("MeuZ9")]
    [InlineData("YyDcTrQ7rdNIE8BuZjr07SOj1fXtgsIpfe9vZqj9bI6KLU6CGtFuwpa3RZwEAS1FWalzE7DbMDglBVwKr5i4ml480bMe7VXifBOXEs7bywBvy9B3Cb4CekieVMA1QO8w1")]
    [InlineData(null)]
    public void DeveRetornarErroCriarNovoUsuarioCommandSenhaInvalido(string senha)
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCpfCommand(senha: senha);

        // Act
        var resultado = command.IsValid;

        // Assert
        Assert.False(resultado);
    }
}