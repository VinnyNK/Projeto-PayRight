using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using PayRight.Extrato.Domain.Entities;
using PayRight.Extrato.Domain.Repositories;
using PayRight.Extrato.Infra.Contexts;

namespace PayRight.Extrato.Infra.Repositories;

public class ContaCorrenteExtratoLeituraRepository : Repository<ContaCorrenteExtrato>, IContaCorrenteExtratoLeituraRepository
{
    public ContaCorrenteExtratoLeituraRepository(ContextoDb db) : base(db)
    {
    }

    public async Task<ContaCorrenteExtrato?> BuscarExtratoPorMesEAno(Guid contaCorrenteId, Guid usuarioId, int mes, int ano)
    {
        return await DbSet.Include(_ => _.Atividades).FirstOrDefaultAsync(_ =>
            _.ContaCorrenteId == contaCorrenteId && _.UsuarioId == usuarioId && _.PeriodoExtrato.Mes == mes &&
            _.PeriodoExtrato.Ano == ano)!;
    }

    public async Task<bool> VerificaSeContaCorrenteEhDoUsuario(Guid contaCorrenteId, Guid usuarioId)
    {
        return await DbSet.FirstOrDefaultAsync(_ => _.ContaCorrenteId == contaCorrenteId && _.UsuarioId == usuarioId) !=
               null;
    }

    public async Task<ContaCorrenteExtrato?> BuscaExtrato(Guid extratoId, Guid usuarioId)
    {
        return await DbSet.AsNoTracking().Include(_ => _.Atividades)
            .FirstOrDefaultAsync(_ => _.Id == extratoId && _.UsuarioId == usuarioId);
    }

    public async Task<ContaCorrenteExtrato?> BuscaExtratoPorData(Guid contaCorrenteId, int mes, int ano)
    {
        return await DbSet.AsNoTracking().Include(_ => _.Atividades)
            .FirstOrDefaultAsync(_ => _.ContaCorrenteId == contaCorrenteId && _.PeriodoExtrato.Mes == mes && _.PeriodoExtrato.Ano == ano);
    }
}