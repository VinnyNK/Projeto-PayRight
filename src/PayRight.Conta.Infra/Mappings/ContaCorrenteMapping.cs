using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayRight.Conta.Domain.Entities;
using PayRight.Conta.Domain.ValueObjects;

namespace PayRight.Conta.Infra.Mappings;

public class ContaCorrenteMapping : IEntityTypeConfiguration<ContaCorrente>
{
    public void Configure(EntityTypeBuilder<ContaCorrente> builder)
    {
        builder.ToTable("contas_correntes");
        builder.HasIndex(_ => _.Id);

        builder.OwnsOne(_ => _.NomeConta, vo =>
        {
            vo.Property(_ => _.Nome).HasColumnName("nome").IsRequired().HasMaxLength(NomeConta.MAX_CARACTERES_NOME);
            vo.Property(_ => _.Apelido).HasColumnName("apelido").HasMaxLength(NomeConta.MAX_CARACTERES_APELIDO);
            vo.Ignore(_ => _.Notifications);
        });

        builder.HasIndex(_ => _.UsuarioId).HasDatabaseName("idx_usuario_id");
        
        builder.Property(_ => _.Ativo).IsRequired().HasDefaultValue(true);
        builder.Property(_ => _.Saldo).IsRequired().HasPrecision(13, 2).HasDefaultValue(0.00);
        builder.Property(_ => _.UltimaAtualizacaoEm).HasColumnName("ultima_alteracao").IsRequired();
        builder.Property(_ => _.CriadoEm).IsRequired().HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        builder.Ignore(_ => _.Notifications);
    }
}