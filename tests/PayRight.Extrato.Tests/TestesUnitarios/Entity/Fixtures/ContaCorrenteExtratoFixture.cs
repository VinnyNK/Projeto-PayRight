using System;
using PayRight.Extrato.Domain.Entities;

namespace PayRight.Extrato.Tests.TestesUnitarios.Entity.Fixtures;

public class ContaCorrenteExtratoFixture : IDisposable
{
    public ContaCorrenteExtrato GerarNovoExtrato(Guid? contaCorrenteId = null, Guid? usuarioId = null, int mes = 12, int? ano = null)
    {
        return new ContaCorrenteExtrato(contaCorrenteId ?? Guid.NewGuid(), usuarioId ?? Guid.NewGuid(), mes, ano ?? DateTime.Now.Year);
    }
    
    public void Dispose()
    {
    }
}