using System;
using PayRight.Extrato.Domain.Commands;
using PayRight.Extrato.Domain.Enums;
using Xunit;

namespace PayRight.Extrato.Tests.TestesUnitarios.Commands.Fixtures;

[CollectionDefinition(nameof(CriarAtividadeContaCorrenteCommandCollection))]
public class CriarAtividadeContaCorrenteCommandCollection : ICollectionFixture<CriarAtividadeContaCorrenteCommandFixture>
{ }

public class CriarAtividadeContaCorrenteCommandFixture
{
    public CriarAtividadeContaCorrenteCommand GerarCommand(Guid? contaCorrenteId = null, Guid? usuarioId = null, string nomeAtividade = "Atividade 1", string? descricaoAtividade = null, decimal valor = 30, TipoAtividade tipoAtividade = TipoAtividade.Despesa, DateOnly? dataEstimado = null)
    {
        return new CriarAtividadeContaCorrenteCommand(contaCorrenteId ?? Guid.NewGuid(), usuarioId ?? Guid.NewGuid(), nomeAtividade, descricaoAtividade, valor, tipoAtividade, dataEstimado ?? DateOnly.FromDateTime(DateTime.Today));
    }
}