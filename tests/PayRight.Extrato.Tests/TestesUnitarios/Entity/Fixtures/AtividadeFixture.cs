using System;
using PayRight.Extrato.Domain.Entities;
using PayRight.Extrato.Domain.Enums;

namespace PayRight.Extrato.Tests.TestesUnitarios.Entity.Fixtures;

public class AtividadeFixture : IDisposable
{
    public Atividade GerarAtividade(string nome = "Padaria", string? descricao = "comprei pao", double valor = 30.50, TipoAtividade tipoAtividade = TipoAtividade.Despesa)
    {
        return new Atividade(nome, descricao, (decimal) valor, tipoAtividade);
    }
    
    public void Dispose()
    {
    }
}