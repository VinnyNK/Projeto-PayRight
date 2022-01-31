using PayRight.Extrato.Domain.Entities;
using PayRight.Extrato.Domain.Repositories;
using PayRight.Extrato.Infra.Contexts;

namespace PayRight.Extrato.Infra.Repositories;

public class AtividadeLeituraRepository : Repository<Atividade>, IAtividadeLeituraRepository
{
    public AtividadeLeituraRepository(ContextoDb db) : base(db)
    {
    }
}