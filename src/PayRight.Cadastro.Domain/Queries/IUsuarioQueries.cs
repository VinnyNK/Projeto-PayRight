using PayRight.Cadastro.Domain.Queries.DTOs;

namespace PayRight.Cadastro.Domain.Queries;

public interface IUsuarioQueries
{
    public Task<UsuarioInfoDTO?> BuscaInfoUsuario(Guid id);

    public Task<UsuarioInfoCompletoDTO?> BuscaUsuarioCompleto(Guid id);
}