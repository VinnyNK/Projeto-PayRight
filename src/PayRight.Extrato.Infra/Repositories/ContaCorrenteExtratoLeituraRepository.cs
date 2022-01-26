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
}