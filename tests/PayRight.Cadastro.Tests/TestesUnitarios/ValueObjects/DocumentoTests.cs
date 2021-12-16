using PayRight.Cadastro.Domain.Enums;
using PayRight.Cadastro.Tests.TestesUnitarios.ValueObjects.Fixtures;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.ValueObjects;

[Collection(nameof(DocumentoCollection))]
public class DocumentoTests
{
    private readonly DocumentoFixture _documentoFixture;

    public DocumentoTests(DocumentoFixture documentoFixture)
    {
        _documentoFixture = documentoFixture;
    }

    [Trait("ValueObject", "Documento")]
    [Fact]
    public void DeveRetornarSucessoNovoDocumentoValido()
    {
        // Arrange
        var documento = _documentoFixture.GerarDocumento();

        // Act
        var resultado = documento.IsValid;

        // Assert
        Assert.True(resultado);
    }
    
    [Trait("ValueObject", "Documento")]
    [Theory]
    [InlineData("91745756000")]
    [InlineData("36734033031")]
    [InlineData("819.377.850-25")]
    public void DeveRetornarSucessoNovoDocumentoTipoCpfValido(string cpf)
    {
        // Arrange
        var documento = _documentoFixture.GerarDocumento(Numero: cpf);

        // Act
        var resultado = documento.IsValid;

        // Assert
        Assert.True(resultado);
    }

    
    [Trait("ValueObject", "Documento")]
    [Theory]
    [InlineData("38.917.542/0001-04")]
    [InlineData("64023530000138")]
    [InlineData("30460042000176")]
    public void DeveRetornarSucessoNovoDocumentoTipoCnpjValido(string cnpj)
    {
        // Arrange
        var documento = _documentoFixture.GerarDocumento(Numero: cnpj, TipoDocumento.CNPJ);

        // Act
        var resultado = documento.IsValid;

        // Assert
        Assert.True(resultado);
    }

    [Trait("ValueObject", "Documento")]
    [Theory]
    [InlineData("9174575600")]
    [InlineData("3673a0330c1")]
    [InlineData("819.377.8466550-25")]
    [InlineData("53307216063")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroNovoDocumentoTipoCpfInvalido(string cpf)
    {
        // Arrange
        var documento = _documentoFixture.GerarDocumento(Numero: cpf);

        // Act
        var resultado = documento.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("ValueObject", "Documento")]
    [Theory]
    [InlineData("778049750001052323")]
    [InlineData("60.97c.47a/0001-03")]
    [InlineData("__60.975.479/0001-03")]
    [InlineData("27404469000143")]
    [InlineData("")]
    [InlineData(null)]
    public void DeveRetornarErroNovoDocumentoTipoCnpjInvalido(string cnpj)
    {
        // Arrange
        var documento = _documentoFixture.GerarDocumento(Numero: cnpj, TipoDocumento.CNPJ);

        // Act
        var resultado = documento.IsValid;

        // Assert
        Assert.False(resultado);
    }

    [Trait("ValueObject", "Documento")]
    [Theory]
    [InlineData("91745756000")]
    [InlineData("36734033031")]
    [InlineData("819.377.850-25")]
    public void DeveRetornarErroNovoDocumentoCpfValidoTipoCnpj(string cpf)
    {
        // Arrange
        var documento = _documentoFixture.GerarDocumento(Numero: cpf, TipoDocumento.CNPJ);

        // Act
        var resultado = documento.IsValid;

        // Assert
        Assert.False(resultado);
    }
    
    [Trait("ValueObject", "Documento")]
    [Theory]
    [InlineData("38.917.542/0001-04")]
    [InlineData("64023530000138")]
    [InlineData("30460042000176")]
    public void DeveRetornarErroNovoDocumentoCnpjValidoTipoCpf(string cnpj)
    {
        // Arrange
        var documento = _documentoFixture.GerarDocumento(Numero: cnpj);

        // Act
        var resultado = documento.IsValid;

        // Assert
        Assert.False(resultado);
    }
}