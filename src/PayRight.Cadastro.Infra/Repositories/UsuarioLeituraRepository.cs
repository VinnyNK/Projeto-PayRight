using Microsoft.EntityFrameworkCore;
using PayRight.Cadastro.Domain.Entities;
using PayRight.Cadastro.Domain.Queries.DTOs;
using PayRight.Cadastro.Domain.Repositories;
using PayRight.Cadastro.Infra.Contexts;

namespace PayRight.Cadastro.Infra.Repositories;

public class UsuarioLeituraRepository : Repository<Usuario>, IUsuarioLeituraRepository
{
    public UsuarioLeituraRepository(ContextoDbCadastroLeitura db) : base(db)
    { }

    public async Task<bool> DocumentoExiste(string numeroDocumento)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(_ => _.Documento.Numero == numeroDocumento) != null;
    }

    public async Task<bool> EmailExiste(string enderecoEmail)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(_ => _.NomeUsuario.Endereco == enderecoEmail) != null;
    }

    public async Task<Usuario?> BuscaUsuario(Guid id)
    {
        return await DbSet.FirstOrDefaultAsync(_ => _.Id == id);
    }

    public async Task<UsuarioInfoDTO?> BuscaUsuarioQuery(Guid id)
    {
        return await DbSet.Select(_ => new UsuarioInfoDTO()
        {
            Id = _.Id,
            NomeCompleto = $"{_.NomeCompleto.PrimeiroNome} {_.NomeCompleto.Sobrenome}",
            EnderecoEmail = _.NomeUsuario.Endereco,
            Ativo = _.Ativo,
            UltimaAtualizacaoEm = _.UltimaAtualizacaoEm
        }).AsNoTracking().FirstOrDefaultAsync(_ => _.Id == id);
    }

    public async Task<UsuarioInfoCompletoDTO?> BuscaUsuarioCompletoQuery(Guid id)
    {
        return await DbSet.Select(_ => new UsuarioInfoCompletoDTO()
        {
            Id = _.Id,
            PrimeiroNome = _.NomeCompleto.PrimeiroNome,
            Sobrenome = _.NomeCompleto.Sobrenome,
            EnderecoEmail = _.NomeUsuario.Endereco,
            TipoDocumento = _.Documento.TipoDocumento,
            NumeroDocumento = _.Documento.Numero,
            Ativo = _.Ativo,
            UltimaAtualizacaoEm = _.UltimaAtualizacaoEm,
            CriadoEm = _.CriadoEm,
        }).AsNoTracking().FirstOrDefaultAsync(_ => _.Id == id);
    }
}