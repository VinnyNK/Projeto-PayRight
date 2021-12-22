using PayRight.Autenticacao.API.DTOs;
using PayRight.Autenticacao.Tests.TestesUnitarios.DTOs.Fixtures;
using Xunit;

namespace PayRight.Autenticacao.Tests.TestesUnitarios.DTOs;

[Collection(nameof(LoginRequestDtoTestCollection))]
public class LoginRequestDtoTests
{
    private readonly LoginRequestDtoTestsFixture _loginRequestDtoTestsFixture;

    public LoginRequestDtoTests(LoginRequestDtoTestsFixture loginRequestDtoTestsFixture)
    {
        _loginRequestDtoTestsFixture = loginRequestDtoTestsFixture;
    }

    [Trait("DTO", "LoginRequest")]
    [Fact]
    public void DeveRetornarSucessoLoginDtoValido()
    {
        // Arrange
        var dto = _loginRequestDtoTestsFixture.GerarNovoLogin();

        // Act
        var resultado = new LoginDto.LoginValidator().Validate(dto).IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("DTO", "LoginRequest")]
    [Theory]
    [InlineData("fulano.gmail.com")]
    [InlineData("email@example.")]
    [InlineData("joao@gmailcom")]
    [InlineData("maria@hotma")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroLoginDtoEmailInvalido(string email)
    {
        // Arrange
        var dto = _loginRequestDtoTestsFixture.GerarNovoLogin(email);

        // Act
        var resultado = new LoginDto.LoginValidator().Validate(dto).IsValid;

        // Assert
        Assert.False(resultado);
    }

    [Trait("DTO", "LoginRequest")]
    [Fact]
    public void DeveRetornarErroLoginDtoSenhaEmBranco()
    {
        // Arrange
        var dto = _loginRequestDtoTestsFixture.GerarNovoLogin(senha: string.Empty);

        // Act
        var resultado = new LoginDto.LoginValidator().Validate(dto).IsValid;

        // Assert
        Assert.False(resultado);
    }
}