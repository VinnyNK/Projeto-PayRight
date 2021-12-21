using PayRight.Cadastro.Domain.Entities;
using PayRight.Cadastro.Domain.Queries.DTOs;

namespace PayRight.Cadastro.Domain.Repositories;

public interface IUsuarioLeituraRepository
{
    Task<bool> DocumentoExiste(string numeroDocumento);

    Task<bool> EmailExiste(string enderecoEmail);
    
    Task<Usuario?> BuscaUsuario(Guid id);
    
    Task<UsuarioInfoDTO?> BuscaUsuarioQuery(Guid id);
    
    Task<UsuarioInfoCompletoDTO?> BuscaUsuarioCompletoQuery(Guid id);
}