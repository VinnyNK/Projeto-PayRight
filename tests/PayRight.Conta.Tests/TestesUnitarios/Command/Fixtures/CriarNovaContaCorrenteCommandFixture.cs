using System;
using PayRight.Conta.Domain.Commands;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Command.Fixtures;

[CollectionDefinition(nameof(CriarNovaContaCorrenteCommandCollection))]
public class CriarNovaContaCorrenteCommandCollection : ICollectionFixture<CriarNovaContaCorrenteCommandFixture>
{ }

public class CriarNovaContaCorrenteCommandFixture : IDisposable
{

    public CriarNovaContaCorrenteCommand GerarCommand(Guid? usuarioId = null, string nome = "Banco Modal",
        string? apelido = "Modal", double saldoInicial = 100.50)
    {
        return new CriarNovaContaCorrenteCommand(usuarioId ?? Guid.NewGuid(), nome, apelido, (decimal) saldoInicial);
    }
    
    public void Dispose()
    {
    }
}