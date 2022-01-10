using System;
using PayRight.Conta.Domain.Messages;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Messages.Fixtures;

[CollectionDefinition(nameof(UsuarioMessageCollection))]
public class UsuarioMessageCollection : ICollectionFixture<UsuarioMessageFixture>
{ }

public class UsuarioMessageFixture : IDisposable
{
    public UsuarioMessage GerarMensagem(Guid? aggregateId = null, string primeiroNome = "Astro")
    {
        return new UsuarioMessage()
        {
            AggregateId = aggregateId ?? Guid.NewGuid(),
            PrimeiroNome = primeiroNome
        };
    }
    
    public void Dispose()
    {
    }
}