﻿using Microsoft.EntityFrameworkCore;
using PayRight.Extrato.Domain.Entities;
using PayRight.Extrato.Domain.Repositories;
using PayRight.Extrato.Infra.Contexts;

namespace PayRight.Extrato.Infra.Repositories;

public class AtividadeLeituraRepository : Repository<Atividade>, IAtividadeLeituraRepository
{
    public AtividadeLeituraRepository(ContextoDb db) : base(db)
    {
    }
    
    public async Task<Atividade?> BuscarAtividade(Guid contaCorrenteId, Guid atividadeId)
    {
        //Todo: Verificar como validar conta corrente id
        return await DbSet.Include(_ => _.Extrato).FirstOrDefaultAsync(_ => _.Id == atividadeId);
    }
}