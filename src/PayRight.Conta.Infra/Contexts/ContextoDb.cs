using Microsoft.EntityFrameworkCore;

namespace PayRight.Conta.Infra.Contexts;

public abstract class ContextoDb : DbContext
{
    protected ContextoDb(DbContextOptions<ContextoDbEscrita> options) : base(options)
    { }
    
    protected ContextoDb(DbContextOptions<ContextoDbLeitura> options) : base(options)
    { }
    
    protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextoDb).Assembly);
    }
}