using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayRight.Extrato.Domain.Entities;
using PayRight.Extrato.Domain.ValueObjects;

namespace PayRight.Extrato.Infra.Mappings;

public class AtividadeMapping : IEntityTypeConfiguration<Atividade>
{
    public void Configure(EntityTypeBuilder<Atividade> builder)
    {
        builder.ToTable("atividades");
        builder.HasIndex(_ => _.Id);
        
        //builder.HasOne(_ => _.Extrato).WithMany(_ => _.Atividades;

        builder.Property(_ => _.Pago).IsRequired();
        builder.Property(_ => _.Valor).HasPrecision(13, 2).HasDefaultValue(0.00);
        builder.Property(_ => _.TipoAtividade).IsRequired();
        builder.Property(_ => _.DataPagamento);
        builder.Property(_ => _.EstimativaPagamento).IsRequired();

        builder.OwnsOne(_ => _.NomeAtividade, vo =>
        {
            vo.Property(_ => _.Nome).IsRequired().HasMaxLength(NomeAtividade.MAX_CARACTERES_NOME);
            vo.Property(_ => _.Descricao).IsRequired().HasMaxLength(NomeAtividade.MAX_CARACTERES_DESCRICAO);
            vo.Ignore(_ => _.Notifications);
        });
        
        builder.Property(_ => _.CriadoEm).IsRequired().HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        builder.Ignore(_ => _.Notifications);
    }
}