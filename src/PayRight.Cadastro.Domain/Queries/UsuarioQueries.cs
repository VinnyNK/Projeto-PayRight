using PayRight.Cadastro.Domain.Queries.DTOs;
using PayRight.Cadastro.Domain.Repositories;

namespace PayRight.Cadastro.Domain.Queries;

public class UsuarioQueries : IUsuarioQueries
{
    private readonly IUsuarioLeituraRepository _usuarioLeituraRepository;

    public UsuarioQueries(IUsuarioLeituraRepository usuarioLeituraRepository)
    {
        _usuarioLeituraRepository = usuarioLeituraRepository;
    }

    public async Task<UsuarioInfoDTO?> BuscaInfoUsuario(Guid id)
    {
        var usuario = await _usuarioLeituraRepository.BuscaUsuarioQuery(id);

        return usuario;
    }

    public async Task<UsuarioInfoCompletoDTO?> BuscaUsuarioCompleto(Guid id)
    {
        var usuario = await _usuarioLeituraRepository.BuscaUsuarioCompletoQuery(id);

        return usuario;
    }
}