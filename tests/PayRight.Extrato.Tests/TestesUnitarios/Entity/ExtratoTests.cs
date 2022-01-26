using System.Collections.Generic;
using PayRight.Extrato.Domain.Entities;
using PayRight.Extrato.Domain.Enums;
using PayRight.Extrato.Tests.TestesUnitarios.Entity.Fixtures;
using Xunit;

namespace PayRight.Extrato.Tests.TestesUnitarios.Entity;

[Collection(nameof(TesteCollection))]
public class ExtratoTests
{
    private readonly AtividadeFixture _atividadeFixture;
    private readonly ContaCorrenteExtratoFixture _contaCorrenteExtratoFixture;

    public ExtratoTests(AtividadeFixture atividadeFixture, ContaCorrenteExtratoFixture contaCorrenteExtratoFixture)
    {
        _atividadeFixture = atividadeFixture;
        _contaCorrenteExtratoFixture = contaCorrenteExtratoFixture;
    }

    [Trait("Entity ", "Extrato")]
    [Fact]
    public void DeveRetornarSucessoExtratoValido()
    {
        // Arrange
        var extrato = _contaCorrenteExtratoFixture.GerarNovoExtrato();

        // Act
        var resultado = extrato.IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("Entity", "Extrato")]
    [Theory]
    [InlineData(-100.00, 50.00, 50.00)]
    [InlineData(-132.50, 50.00, 50.00, 32.5)]
    public void DeveRetornarValorEsperadoAoAdicionarAtividadeDespesaNaoPaga(decimal valorEsperado, params double[] valores)
    {
        // Arrange
        var extrato = _contaCorrenteExtratoFixture.GerarNovoExtrato();
        foreach (var valor in valores)
            extrato.AdicionarAtividade(_atividadeFixture.GerarAtividade(valor: valor));

        // Act & Assert
        Assert.Equal(valorEsperado, extrato.TotalEstimado);
    }

    [Trait("Entity", "Extrato")]
    [Fact]
    public void DeveRetornarValorTotalAoAdicionarAtividadesPagas()
    {
        // Arrange
        var extrato = _contaCorrenteExtratoFixture.GerarNovoExtrato();
        var atividade1 = _atividadeFixture.GerarAtividade(valor: 30);
        var atividade2 = _atividadeFixture.GerarAtividade(valor: 30);
        var atividade3 = _atividadeFixture.GerarAtividade(tipoAtividade: TipoAtividade.Receita, valor: 100);
        
        // Act
        extrato.AdicionarAtividade(atividade1);
        extrato.AdicionarAtividade(atividade2);
        atividade2.PagarAtividade();
        extrato.AdicionarAtividade(atividade3);
        atividade3.PagarAtividade();
        
        
        // Assert
        Assert.Equal(70, extrato.Total);
        Assert.Equal(40, extrato.TotalEstimado);
        
    }

    [Trait("Entity", "Extrato")]
    [Fact]
    public void DeveRetornarValorEsperadoAoRemoverAtividade()
    {
        // Arrange
        var extrato = _contaCorrenteExtratoFixture.GerarNovoExtrato();
        var atividade1 = _atividadeFixture.GerarAtividade(valor: 30);
        var atividade2 = _atividadeFixture.GerarAtividade(valor: 30);
        var atividade3 = _atividadeFixture.GerarAtividade(tipoAtividade: TipoAtividade.Receita, valor: 100);
        var atividade4 = _atividadeFixture.GerarAtividade(tipoAtividade: TipoAtividade.Receita, valor: 325);
        
        // Act
        extrato.AdicionarAtividade(atividade1);
        extrato.AdicionarAtividade(atividade2);
        atividade2.PagarAtividade();
        extrato.AdicionarAtividade(atividade3);
        atividade3.PagarAtividade();
        extrato.AdicionarAtividade(atividade4);
        atividade4.PagarAtividade();
        extrato.RemoveAtividade(atividade2);
        extrato.RemoveAtividade(atividade4);
        
        // Assert
        Assert.Equal(100, extrato.Total);
        Assert.Equal(70, extrato.TotalEstimado);
    }

    [Trait("Entity", "Extrato")]
    [Fact]
    public void DeveRetornarErroAoExcluirAtividadeNaoExistente()  
    {
        // Arrange
        var extrato = _contaCorrenteExtratoFixture.GerarNovoExtrato();
        var atividade1 = _atividadeFixture.GerarAtividade(valor: 30);
        var atividade2 = _atividadeFixture.GerarAtividade(valor: 30);
        
        // Act
        extrato.AdicionarAtividade(atividade1);
        extrato.RemoveAtividade(atividade2);
        var resultado = extrato.IsValid;

        // Assert
        Assert.False(resultado);
        Assert.IsAssignableFrom<IReadOnlyCollection<Atividade>>(extrato.Atividades);
    }

    [Trait("Entity", "Extrato")]
    [Fact]
    public void DeveRetornarNovoTotalETotalEstimadoAoAlterarValorDeAtividadePagaENaoPaga()
    {
        // Arrange
        var extrato = _contaCorrenteExtratoFixture.GerarNovoExtrato();
        var atividade1 = _atividadeFixture.GerarAtividade(valor: 30);
        var atividade2 = _atividadeFixture.GerarAtividade(valor: 30);
        var atividade3 = _atividadeFixture.GerarAtividade(tipoAtividade: TipoAtividade.Receita, valor: 100);

        // Act
        extrato.AdicionarAtividade(atividade1);
        extrato.AdicionarAtividade(atividade2);
        atividade2.PagarAtividade();
        extrato.AdicionarAtividade(atividade3);
        atividade1.AlterarValorAtividade(31);
        atividade2.AlterarValorAtividade(32);
        
        // Assert
        Assert.Equal(-32, extrato.Total);
        Assert.Equal(37, extrato.TotalEstimado);
    }
}