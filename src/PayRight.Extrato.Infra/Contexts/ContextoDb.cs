using Microsoft.EntityFrameworkCore;
using PayRight.Extrato.Domain.Entities;

namespace PayRight.Extrato.Infra.Contexts;

public abstract class ContextoDb : DbContext
{
    protected ContextoDb(DbContextOptions<ContextoDbEscrita> options) : base(options)
    { }
    
    protected ContextoDb(DbContextOptions<ContextoDbLeitura> options) : base(options)
    { }
    
    protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore(
            typeof(Domain.Entities.Extrato)
        );
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextoDb).Assembly);
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}