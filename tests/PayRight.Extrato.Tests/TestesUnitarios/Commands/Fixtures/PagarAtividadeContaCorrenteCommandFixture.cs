using System;
using PayRight.Extrato.Domain.Commands;
using Xunit;

namespace PayRight.Extrato.Tests.TestesUnitarios.Commands.Fixtures;

[CollectionDefinition(nameof(PagarAtividadeContaCorrenteCommandCollection))]
public class PagarAtividadeContaCorrenteCommandCollection : ICollectionFixture<PagarAtividadeContaCorrenteCommandFixture>
{ }
    
public class PagarAtividadeContaCorrenteCommandFixture
{
    public PagarAtividadeContaCorrenteCommand GerarCommand(Guid? usuarioId = null, Guid? contaCorrenteId = null, Guid? atividadeId = null, DateOnly? dataPagamento = null)
    {
        return new PagarAtividadeContaCorrenteCommand(usuarioId ?? Guid.NewGuid(), contaCorrenteId ?? Guid.NewGuid(), atividadeId ?? Guid.NewGuid(), dataPagamento ?? DateOnly.FromDateTime(DateTime.Today));
    }
}