using System;
using PayRight.Extrato.Domain.ValueObjects;
using Xunit;

namespace PayRight.Extrato.Tests.TestesUnitarios.ValueObjects.Fixture;

[CollectionDefinition(nameof(NomeAtividadeCollection))]
public class NomeAtividadeCollection : ICollectionFixture<NomeAtividadeFixture>
{ }

public class NomeAtividadeFixture : IDisposable
{
    public NomeAtividade GerarNomeAtividade(string nome = "Padaria", string? descricao = "comprei pao")
    {
        return new NomeAtividade(nome, descricao);
    }
    
    public void Dispose()
    {
    }
}