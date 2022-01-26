using System;
using PayRight.Extrato.Domain.ValueObjects;
using Xunit;

namespace PayRight.Extrato.Tests.TestesUnitarios.ValueObjects.Fixture;


[CollectionDefinition(nameof(PeriodoExtratoCollection))]
public class PeriodoExtratoCollection : ICollectionFixture<PeriodoExtratoFixture>
{ }

public class PeriodoExtratoFixture : IDisposable
{
    public PeriodoExtrato GerarPeriodoExtrato(int mes = 12, int? ano = null)
    {
        return new PeriodoExtrato(mes, ano ?? DateTime.Now.Year);
    }
    
    public void Dispose()
    {
    }
}