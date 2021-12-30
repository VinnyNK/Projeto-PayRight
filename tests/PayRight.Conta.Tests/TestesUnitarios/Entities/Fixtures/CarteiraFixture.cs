using System;
using PayRight.Conta.Domain.Entities;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Entities.Fixtures;

[CollectionDefinition(nameof(CarteiraCollection))]
public class CarteiraCollection : ICollectionFixture<CarteiraFixture>
{ }

public class CarteiraFixture : IDisposable
{
    public Carteira GerarNovaCarteira(Guid? usuarioId = null)
    {
        var carteira = new Carteira(usuarioId ?? Guid.NewGuid());

        return carteira;
    }
    
    
    public void Dispose()
    {
    }
}