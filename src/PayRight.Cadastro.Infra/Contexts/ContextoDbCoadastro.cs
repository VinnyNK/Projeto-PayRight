using Microsoft.EntityFrameworkCore;

namespace PayRight.Cadastro.Infra.Contexts;

public abstract class ContextoDbCoadastro : DbContext
{
    protected ContextoDbCoadastro(DbContextOptions<ContextoDbCadastroLeitura> options) : base(options)
    { }

    protected ContextoDbCoadastro(DbContextOptions<ContextoDbCadastroEscrita> options) : base(options)
    { }

    protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextoDbCoadastro).Assembly);
    }
}