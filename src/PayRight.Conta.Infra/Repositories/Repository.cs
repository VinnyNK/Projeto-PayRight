using Microsoft.EntityFrameworkCore;
using PayRight.Conta.Infra.Contexts;
using PayRight.Shared.Entities;

namespace PayRight.Conta.Infra.Repositories;

public abstract class Repository<T> : IDisposable where T : Entity
{
    protected readonly ContextoDb Db;
    protected readonly DbSet<T> DbSet;
    
    protected Repository(ContextoDb db)
    {
        Db = db;
        DbSet = db.Set<T>();
    }

    public void Dispose() => Db?.Dispose();
}