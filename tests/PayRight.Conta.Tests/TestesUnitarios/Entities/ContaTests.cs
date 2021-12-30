using System;
using PayRight.Conta.Domain.ValueObjects;
using PayRight.Conta.Tests.TestesUnitarios.Entities.Fixtures;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Entities;

[Collection(nameof(ContaCorrenteCollection))]
public class ContaTests
{
    private readonly ContaCorrenteFixture _contaCorrenteFixture;

    public ContaTests(ContaCorrenteFixture contaCorrenteFixture)
    {
        _contaCorrenteFixture = contaCorrenteFixture;
    }

    [Trait("Entity", "Conta")]
    [Fact]
    public void DeveRetornarSucessoCriarContaQualquerTipoValida()
    {
        // Arrange & Act
        var conta = _contaCorrenteFixture.GerarNovoContaCorrente();

        // Assert
        Assert.True(conta.Ativo);
        Assert.IsType<Guid>(conta.UsuarioId);
        Assert.True(conta.IsValid);
        Assert.NotNull(conta.NomeConta);
        Assert.Equal(0, conta.Saldo);
        Assert.InRange(conta.UltimaAtualizacaoEm, conta.CriadoEm, conta.CriadoEm.AddHours(1));
    }

    [Trait("Entity", "Conta")]
    [Theory]
    [InlineData("Nome Alterado", "Apelido")]
    [InlineData("Nome Sem Apelido", null)]
    public void DeveRetornarSucessoAlterarNomeContaValidoQualquerTipo(string nome, string? apelido)
    {
        // Arrange
        var conta = _contaCorrenteFixture.GerarNovoContaCorrente();
        conta.AlterarNomeConta(new NomeConta(nome, apelido));
        
        // Act
        var resultado = conta.IsValid;

        // Assert
        Assert.True(resultado);
    }
    
    [Trait("Entity", "Conta")]
    [Theory]
    [InlineData("NedANTiVICUTACkOCaLiNeORa", "nelphydrYSIblenD")]
    [InlineData("", null)]
    [InlineData(null, null)]
    [InlineData("AB", null)]
    public void DeveRetornarErroAlterarNomeContaInvalidoQualquerTipo(string nome, string? apelido)
    {
        // Arrange
        var conta = _contaCorrenteFixture.GerarNovoContaCorrente();
        conta.AlterarNomeConta(new NomeConta(nome, apelido));
        
        // Act
        var resultado = conta.IsValid;

        // Assert
        Assert.False(resultado);
    }

    [Trait("Entity", "Conta")]
    [Fact]
    public void DeveRetornarFalsoAoDesativarContaAtiva()
    {
        // Arrange
        var conta = _contaCorrenteFixture.GerarNovoContaCorrente();
        conta.AtivarConta();
        
        // Act
        conta.DesativarConta();

        // Assert
        Assert.False(conta.Ativo);
    }
    
    [Trait("Entity", "Conta")]
    [Fact]
    public void DeveRetornarAtivoAoAtivarContaDesabilitada()
    {
        // Arrange
        var conta = _contaCorrenteFixture.GerarNovoContaCorrente();
        conta.DesativarConta();
        
        // Act
        conta.AtivarConta();

        // Assert
        Assert.True(conta.Ativo);
    }
}