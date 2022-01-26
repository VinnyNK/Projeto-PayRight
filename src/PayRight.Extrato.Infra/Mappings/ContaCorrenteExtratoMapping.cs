using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayRight.Extrato.Domain.Entities;

namespace PayRight.Extrato.Infra.Mappings;

public class ContaCorrenteExtratoMapping : IEntityTypeConfiguration<ContaCorrenteExtrato>
{
    public void Configure(EntityTypeBuilder<ContaCorrenteExtrato> builder)
    {
        builder.ToTable("extratos_contas_corrente");
        builder.HasIndex(_ => _.Id);

        builder.Property(_ => _.Total).IsRequired().HasPrecision(13, 2).HasDefaultValue(0.00);
        builder.Property(_ => _.TotalEstimado).IsRequired().HasPrecision(13, 2).HasDefaultValue(0.00);

        builder.OwnsOne(_ => _.PeriodoExtrato, vo =>
        {
            vo.Property(_ => _.Mes).HasColumnName("mes").IsRequired().HasMaxLength(2);
            vo.Property(_ => _.Ano).HasColumnName("ano").IsRequired().HasMaxLength(4);
            vo.Ignore(_ => _.Notifications);
        });

        builder.HasIndex(_ => _.ContaCorrenteId).HasDatabaseName("idx_conta_corrente_id");
        builder.HasIndex(_ => _.UsuarioId).HasDatabaseName("idx_usuario_id");

        builder.Property(_ => _.CriadoEm).IsRequired().HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        builder.Ignore(_ => _.Notifications);
    }
}