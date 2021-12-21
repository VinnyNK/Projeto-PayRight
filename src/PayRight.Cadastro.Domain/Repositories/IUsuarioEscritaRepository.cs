using PayRight.Cadastro.Domain.Entities;
using PayRight.Cadastro.Domain.ValueObjects;

namespace PayRight.Cadastro.Domain.Repositories;

public interface IUsuarioEscritaRepository
{
    Task CriarNovoUsuario(Usuario usuario);

    void AtualizarUsuario(Usuario usuario);

    Task<bool> Commit();
}