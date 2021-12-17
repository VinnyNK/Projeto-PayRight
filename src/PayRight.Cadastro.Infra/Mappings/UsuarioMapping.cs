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

        builder.Property(_ => _.NomeCompleto.PrimeiroNome).HasField("primeiro_nome").IsRequired().HasMaxLength(64);
        builder.Property(_ => _.NomeCompleto.Sobrenome).HasField("sobrenome").HasMaxLength(64);
        builder.Property(_ => _.Documento.Numero).HasField("documento").IsRequired().HasMaxLength(14);
        builder.Property(_ => _.Documento.TipoDocumento).HasField("tipo_documento").IsRequired();
        builder.Property(_ => _.NomeUsuario.Endereco).HasField("email").IsRequired().HasMaxLength(80);
        builder.Property(_ => _.Senha).IsRequired();
        builder.Property(_ => _.Ativo).IsRequired().HasDefaultValue(true);
        builder.Property(_ => _.UltimaAtualizacaoEm).IsRequired();
        builder.Property(_ => _.CriadoEm).IsRequired().HasDefaultValue("NOW();");

        builder.HasIndex(_ => _.Documento.Numero).HasName("idx_numero_documento").IsUnique();
        builder.HasIndex(_ => _.NomeUsuario.Endereco).HasName("idx_email").IsUnique();
    }
}