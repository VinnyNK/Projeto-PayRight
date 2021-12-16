using PayRight.Cadastro.Domain.Entities;
using PayRight.Cadastro.Domain.ValueObjects;

namespace PayRight.Cadastro.Domain.Repositories;

public interface IUsuarioRepository
{
    Task<bool> DocumentoExiste(string numeroDocumento);

    Task<bool> EmailExiste(string enderecoEmail);

    Task<Usuario?> BuscaUsuario(Guid id);

    Task<Usuario?> BuscaUsuarioCompleto(Guid id);
    
    Task CriarNovoUsuario(Usuario usuario);

    Task AtualizarUsuario(Usuario usuario);

    Task<bool> Commit();
}