using Microsoft.EntityFrameworkCore;
using PayRight.Autenticacao.API.Models;
using PayRight.Shared.Entities;

namespace PayRight.Autenticacao.API.Context;

public class ContextoDbLeitura : DbContext
{
    public ContextoDbLeitura(DbContextOptions<ContextoDbLeitura> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextoDbLeitura).Assembly);
        modelBuilder.Entity<Usuario>().HasNoKey().Ignore(c => c.Notifications);
    }
}