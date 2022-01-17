using Microsoft.EntityFrameworkCore;
using PayRight.Conta.Domain.Entities;
using PayRight.Conta.Domain.Repositories;
using PayRight.Conta.Infra.Contexts;

namespace PayRight.Conta.Infra.Repositories;

public class ContaCorrenteLeituraRepository : Repository<ContaCorrente>, IContaCorrenteLeituraRepository
{
    public ContaCorrenteLeituraRepository(ContextoDbLeitura db) : base(db)
    {
    }

    public async Task<bool> NomeContaExisteParaUsuario(Guid usuarioId, string nome)
    {
        return await DbSet.AsNoTracking()
            .FirstOrDefaultAsync(_ => _.UsuarioId == usuarioId && _.NomeConta.Nome == nome) != null;
    }

    public async Task<IEnumerable<ContaCorrente>> BuscarContasCorrente(Guid usuarioId)
    {
        return await DbSet.Where(_ => _.UsuarioId == usuarioId).AsNoTracking().ToListAsync();
    }

    public async Task<ContaCorrente?> BuscarContaCorrente(Guid usuarioId, Guid contaCorrenteId)
    {
        return await DbSet.FirstOrDefaultAsync(_ => _.Id == contaCorrenteId && _.UsuarioId == usuarioId);
    }
}