using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayRight.Cadastro.Domain.Entities;

namespace PayRight.Cadastro.Infra.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");
        builder.HasIndex(_ => _.Id);
        
        builder.OwnsOne(_ => _.NomeCompleto, vo =>
        {
            vo.Property(_ => _.PrimeiroNome).HasColumnName("primeiro_nome").IsRequired().HasMaxLength(64);
            vo.Property(_ => _.Sobrenome).HasColumnName("sobrenome").HasMaxLength(64);
            vo.Ignore(_ => _.Notifications);
        });
        builder.OwnsOne(_ => _.Documento, vo =>
        {
            vo.Property(_ => _.Numero).HasColumnName("documento").IsRequired().HasMaxLength(14);
            vo.Property(_ => _.TipoDocumento).HasColumnName("tipo_documento").IsRequired();
            vo.HasIndex(_ => _.Numero).HasDatabaseName("idx_numero_documento").IsUnique();
            vo.Ignore(_ => _.Notifications);
        });
        builder.OwnsOne(_ => _.NomeUsuario, vo =>
        {
            vo.Property(_ => _.Endereco).HasColumnName("email").IsRequired().HasMaxLength(80);
            vo.HasIndex(_ => _.Endereco).HasDatabaseName("idx_email").IsUnique();
            vo.Ignore(_ => _.Notifications);
        });
        builder.Property(_ => _.Senha).IsRequired();
        builder.Property(_ => _.Ativo).IsRequired().HasDefaultValue(true);
        builder.Property(_ => _.UltimaAtualizacaoEm).IsRequired();
        builder.Property(_ => _.CriadoEm).IsRequired().HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        builder.Ignore(_ => _.ConfirmacaoSenha);
        builder.Ignore(_ => _.Notifications);
    }
}