using Microsoft.EntityFrameworkCore;
using PayRight.Extrato.Domain.Entities;
using PayRight.Extrato.Domain.Repositories;
using PayRight.Extrato.Infra.Contexts;

namespace PayRight.Extrato.Infra.Repositories;

public class AtividadeEscritaRepository : Repository<Atividade>, IAtividadeEscritaRepository
{
    public AtividadeEscritaRepository(ContextoDb db) : base(db)
    {
    }

    public async Task AdicionaAtividade(Atividade atividade)
    {
        await DbSet.AddAsync(atividade);
        Db.Entry(atividade.NomeAtividade).State = EntityState.Added;
        // Todo: verifica o motivo de alterar o estado de um value obkect
    }
}