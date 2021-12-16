using System;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.ValueObjects.Fixtures;

[CollectionDefinition(nameof(NomeCompletoCollection))]
public class NomeCompletoCollection : ICollectionFixture<NomeCompletoFixture>
{ }

public class NomeCompletoFixture : IDisposable
{
    public Domain.ValueObjects.NomeCompleto GerarNomeCompleto(string primeiroNome = "Vinicius", string sobrenome = "do Nascimento Kerschner")
    {
        return new Domain.ValueObjects.NomeCompleto(primeiroNome, sobrenome);
    }
    
    public void Dispose()
    {
    }
}