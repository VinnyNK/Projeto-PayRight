using System;
using PayRight.Conta.Domain.Commands;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Command.Fixtures;

[CollectionDefinition(nameof(SaldoContaCorrenteCommandCollection))]
public class SaldoContaCorrenteCommandCollection : ICollectionFixture<SomarSaldoContaCorrenteCommandFixture>, ICollectionFixture<SubtrairSaldoContaCorrenteCommandFixture>
{ }

public class SomarSaldoContaCorrenteCommandFixture : IDisposable
{
    public SomaSaldoContaCorrenteCommand GerarCommand(Guid? usuarioId = null, Guid? contaCorrenteId = null,
        decimal valor = 100)
    {
        return new SomaSaldoContaCorrenteCommand(usuarioId ?? Guid.NewGuid(), contaCorrenteId ?? Guid.NewGuid(),
            valor);
    }
    
    public void Dispose()
    {
    }
}

public class SubtrairSaldoContaCorrenteCommandFixture : IDisposable
{
    public SubtrairSaldoContaCorrenteCommand GerarCommand(Guid? usuarioId = null, Guid? contaCorrenteId = null,
        decimal valor = 100)
    {
        return new SubtrairSaldoContaCorrenteCommand(usuarioId ?? Guid.NewGuid(), contaCorrenteId ?? Guid.NewGuid(),
            valor);
    }

    public void Dispose()
    {
    }
}