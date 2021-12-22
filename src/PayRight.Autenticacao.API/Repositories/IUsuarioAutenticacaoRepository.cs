using PayRight.Autenticacao.API.Models;

namespace PayRight.Autenticacao.API.Repositories;

public interface IUsuarioAutenticacaoRepository
{
    Task<Usuario?> BuscaUsuarioPorEmail(string email);
}