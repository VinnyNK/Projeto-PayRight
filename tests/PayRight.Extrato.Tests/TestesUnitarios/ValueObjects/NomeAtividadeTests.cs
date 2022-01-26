using PayRight.Extrato.Tests.TestesUnitarios.ValueObjects.Fixture;
using Xunit;

namespace PayRight.Extrato.Tests.TestesUnitarios.ValueObjects;

[Collection(nameof(NomeAtividadeCollection))]
public class NomeAtividadeTests
{
    private readonly NomeAtividadeFixture _nomeAtividadeFixture;

    public NomeAtividadeTests(NomeAtividadeFixture nomeAtividadeFixture)
    {
        _nomeAtividadeFixture = nomeAtividadeFixture;
    }

    [Trait("ValueObject", "NomeAtividade")]
    [Fact]
    public void DeveRetornarSucessoNomeAtividadeValido()
    {
        // Arrange
        var nomeAtividade = _nomeAtividadeFixture.GerarNomeAtividade();

        // Act
        nomeAtividade.Validar();
        var resultado = nomeAtividade.IsValid;
        
        // Assert
        Assert.True(resultado);
    }

    [Trait("ValueObject", "NomeAtividade")]
    [Theory]
    [InlineData("", "teste123")]
    [InlineData("Ab", null)]
    [InlineData("vxmHvejaYRbA4kr2LtcLUmmweR78eL8YJ", null)]
    [InlineData("Padaria", "WbZ4XtBp6JjfSiNoHaFdxpsFhEovTDPSVLNA6Wg857Be4CHCsxL45W9Zba8ZSPz3kw3f6FBNFaQdZcLtzqCCnNvv2gmRZMuGoSUGhzzPwYufiUJyXb59c68SQSP2qc6t8")]
    [InlineData("Padaria", "Ab")]
    public void DeveRetornarErroNomeAtividadeInvalido(string nome, string? descricao)
    {
        // Arrange
        var nomeAtividade = _nomeAtividadeFixture.GerarNomeAtividade(nome, descricao);

        // Act
        nomeAtividade.Validar();
        var resultado = nomeAtividade.IsValid;

        // Assert
        Assert.False(resultado);
    }
}