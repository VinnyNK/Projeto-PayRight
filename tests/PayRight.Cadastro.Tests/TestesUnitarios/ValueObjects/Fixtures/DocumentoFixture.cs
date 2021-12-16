using System;
using PayRight.Cadastro.Domain.Enums;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.ValueObjects.Fixtures;

[CollectionDefinition(nameof(DocumentoCollection))]
public class DocumentoCollection : ICollectionFixture<DocumentoFixture>
{ }

public class DocumentoFixture : IDisposable
{
    public Domain.ValueObjects.Documento GerarDocumento(string Numero = "59529467044",
        TipoDocumento tipoDocumento = TipoDocumento.CPF)
    {
        return new Domain.ValueObjects.Documento(Numero, tipoDocumento);
    }
    
    public void Dispose()
    {
    }
}