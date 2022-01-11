using PayRight.Conta.Domain.Entities;
using PayRight.Conta.Domain.Repositories;
using PayRight.Conta.Infra.Contexts;

namespace PayRight.Conta.Infra.Repositories;

public class ContaCorrenteEscritaRepository : Repository<ContaCorrente>, IContaCorrenteEscritaRepository
{
    public ContaCorrenteEscritaRepository(ContextoDbEscrita db) : base(db)
    {
    }

    public async Task CriarContaCorrente(ContaCorrente contaCorrente)
    {
        await DbSet.AddAsync(contaCorrente);
    }

    public async Task<bool> Commit()
    {
        return await Db.SaveChangesAsync() != 0;
    }
}