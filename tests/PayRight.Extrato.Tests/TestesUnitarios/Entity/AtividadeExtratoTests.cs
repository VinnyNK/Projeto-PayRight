using PayRight.Extrato.Domain.Enums;
using PayRight.Extrato.Tests.TestesUnitarios.Entity.Fixtures;
using Xunit;

namespace PayRight.Extrato.Tests.TestesUnitarios.Entity;

[Collection(nameof(TesteCollection))]
public class AtividadeExtratoTests
{
    private readonly AtividadeFixture _atividadeFixture;
    private readonly ContaCorrenteExtratoFixture _contaCorrenteExtratoFixture;

    public AtividadeExtratoTests(AtividadeFixture atividadeFixture, ContaCorrenteExtratoFixture contaCorrenteExtratoFixture)
    {
        _atividadeFixture = atividadeFixture;
        _contaCorrenteExtratoFixture = contaCorrenteExtratoFixture;
    }

    [Trait("Entity", "Atividade")]
    [Fact]
    public void DeveRetornarSucessoAtividadeValida()
    {
        // Arrange  
        var atividade = _atividadeFixture.GerarAtividade();

        // Act
        atividade.Validar();
        var resultado = atividade.IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("Entity ", "Atividade")]
    [Fact]
    public void DeveRetornarErroAtividadeValorInvalido()
    {
        // Arrange
        var atividade = _atividadeFixture.GerarAtividade(valor: -1);
        
        // Act
        atividade.Validar();
        var resultado = atividade.IsValid;

        // Assert
        Assert.False(resultado);
    }

    [Trait("Entity", "Atividade")]
    [Fact]
    public void DeveTerExtratoAoAdicionarExtrato()
    {
        // Arrange
        var atividade = _atividadeFixture.GerarAtividade();
        
        // Act
        atividade.AdicionarExtrato(_contaCorrenteExtratoFixture.GerarNovoExtrato());

        // Assert
        Assert.IsAssignableFrom<Domain.Entities.Extrato>(atividade.Extrato);
    }

    [Trait("Entity", "Atividade")]
    [Fact]
    public void DeveRetornarNovoNomeDaAtividadeAoAlterarNomeValido()    
    {
        // Arrange
        var atividade = _atividadeFixture.GerarAtividade();

        // Act
        atividade.AlterarNomeAtividade("Padaria", null);

        // Assert
        Assert.Equal("Padaria", atividade.NomeAtividade.Nome);
        Assert.Null(atividade.NomeAtividade.Descricao);
    }

    [Trait("Entity", "Atividade")]
    [Fact]
    public void DeveAlterarValorDaAtividadeDespesaAoChamarMetodoDeAlteracao()
    {
        // Arrange
        var atividade = _atividadeFixture.GerarAtividade();
        atividade.AdicionarExtrato(_contaCorrenteExtratoFixture.GerarNovoExtrato());
        
        // Act
        atividade.AlterarValorAtividade(150);
        
        // Assert
        Assert.Equal(150, atividade.Valor);
    }
    
    [Trait("Entity", "Atividade")]
    [Fact]
    public void DeveAlterarValorDaAtividadeReceitaAoChamarMetodoDeAlteracao()
    {
        // Arrange
        var atividade = _atividadeFixture.GerarAtividade(tipoAtividade: TipoAtividade.Receita);
        atividade.AdicionarExtrato(_contaCorrenteExtratoFixture.GerarNovoExtrato());
        
        // Act
        atividade.AlterarValorAtividade(150);
        
        // Assert
        Assert.Equal(150, atividade.Valor);
    }

    [Trait("Entity", "Atividade")]
    [Fact]
    public void DeveRetornarErroAoAlterarValorParaValorInvalido()
    {
        // Arrange
        var atividade = _atividadeFixture.GerarAtividade();
        atividade.AdicionarExtrato(_contaCorrenteExtratoFixture.GerarNovoExtrato());
        // Act
        atividade.AlterarValorAtividade(-50);
        var resultado = atividade.IsValid;
        
        // Assert
        Assert.False(resultado);
    }

    [Trait("Entity", "Atividade")]
    [Fact]
    public void DeveMaterValorOriginalAoAlterarValorSemExtrato()
    {
        // Arrange
        var atividade = _atividadeFixture.GerarAtividade();
        var original = atividade.Valor;
        
        // Act
        atividade.AlterarValorAtividade(-50);

        // Assert
        Assert.Equal(original, atividade.Valor);
    }

    [Trait("Entity", "Atividade")]
    [Fact]
    public void DeveAlterarParaPagoAtividadeNaoPagaAoChamarMetodoDePagamento()
    {
        // Arrange
        var atividade = _atividadeFixture.GerarAtividade();
        atividade.AdicionarExtrato(_contaCorrenteExtratoFixture.GerarNovoExtrato());

        // Act 
        atividade.PagarAtividade();

        // Assert
        Assert.True(atividade.Pago);
    }

    [Trait("Entity", "Atividade")]
    [Fact]
    public void DeveAlterarParaNaoPagoAtividadePagaAoChamarMetodoDeRetornoDePagamento()
    {
        // Arrange
        var atividade = _atividadeFixture.GerarAtividade();
        atividade.AdicionarExtrato(_contaCorrenteExtratoFixture.GerarNovoExtrato());
        atividade.PagarAtividade();

        // Act 
        atividade.RetornarPagamento();

        // Assert
        Assert.False(atividade.Pago);
    }

    [Trait("Entity", "Atividade")]
    [Fact]
    public void DeveManterAtividadeNaoPagaAoChamarMetodoDePagamentoSemExtrato()
    {
        // Arrange
        var atividade = _atividadeFixture.GerarAtividade();

        // Act 
        atividade.RetornarPagamento();

        // Assert
        Assert.False(atividade.Pago);
    }
    
    [Trait("Entity", "Atividade")]
    [Fact]
    public void DeveManterAtividadeNaoPagaAoChamarMetodoDePagamentoNaoPago()
    {
        // Arrange
        var atividade = _atividadeFixture.GerarAtividade();
        atividade.AdicionarExtrato(_contaCorrenteExtratoFixture.GerarNovoExtrato());
        
        // Act 
        atividade.RetornarPagamento();

        // Assert
        Assert.False(atividade.Pago);
    }
    
    [Trait("Entity", "Atividade")]
    [Fact]
    public void DeveManterAtividadePagaAoChamarMetodoDePagarAtividade()
    {
        // Arrange
        var atividade = _atividadeFixture.GerarAtividade();
        atividade.AdicionarExtrato(_contaCorrenteExtratoFixture.GerarNovoExtrato());
        atividade.PagarAtividade();

        // Act 
        atividade.PagarAtividade();

        // Assert
        Assert.True(atividade.Pago);
    }
}