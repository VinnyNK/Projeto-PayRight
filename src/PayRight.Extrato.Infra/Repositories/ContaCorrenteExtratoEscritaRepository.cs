using Microsoft.EntityFrameworkCore;
using PayRight.Extrato.Domain.Entities;
using PayRight.Extrato.Domain.Repositories;
using PayRight.Extrato.Infra.Contexts;

namespace PayRight.Extrato.Infra.Repositories;

public class ContaCorrenteExtratoEscritaRepository : Repository<ContaCorrenteExtrato>, IContaCorrenteExtratoEscritaRepository
{
    public ContaCorrenteExtratoEscritaRepository(ContextoDb db) : base(db)
    {
    }

    public async Task AdicionaExtrato(ContaCorrenteExtrato extrato)
    {
        await DbSet.AddAsync(extrato);
    }

    public void AtualizarExtrato(ContaCorrenteExtrato extrato)
    {
        DbSet.Update(extrato);
    }

    public async Task AdicionaOuAtualizaExtrato(ContaCorrenteExtrato extrato)
    {
        if (Db.Entry(extrato).State == EntityState.Added)
            await DbSet.AddAsync(extrato);
        DbSet.Update(extrato);
    }
}