using System;
using PayRight.Conta.Domain.Entities;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Entities.Fixtures;

[CollectionDefinition(nameof(ContaCorrenteCollection))]
public class ContaCorrenteCollection : ICollectionFixture<ContaCorrenteFixture>
{ }

public class ContaCorrenteFixture : IDisposable
{
    public ContaCorrente GerarNovoContaCorrente(Guid? usuarioId = null, string nome = "Nubank", string? apelido = "roxinho")
    {
        var contaCorrente = new ContaCorrente(usuarioId ?? Guid.NewGuid(), nome, apelido);

        return contaCorrente;
    }
    
    
    public void Dispose()
    {
    }
}