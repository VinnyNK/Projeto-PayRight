using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayRight.Autenticacao.API.Models;

namespace PayRight.Autenticacao.API.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");
        builder.HasIndex(_ => _.Id);

        
        builder.Property(_ => _.Email).HasColumnName("email").IsRequired().HasMaxLength(80);
        builder.Property(_ => _.Senha).IsRequired();
        builder.Property(_ => _.CriadoEm).IsRequired().HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        
        builder.HasIndex(_ => _.Email).HasDatabaseName("idx_email").IsUnique();
    }
}