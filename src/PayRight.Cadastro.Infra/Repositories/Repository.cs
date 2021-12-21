using Microsoft.EntityFrameworkCore;
using PayRight.Cadastro.Infra.Contexts;
using PayRight.Shared.Entities;

namespace PayRight.Cadastro.Infra.Repositories;

public abstract class Repository<T> where T : Entity
{
    protected readonly ContextoDbCoadastro Db;
    protected readonly DbSet<T> DbSet;
    
    protected Repository(ContextoDbCoadastro db)
    {
        Db = db;
        DbSet = db.Set<T>();
    }
}