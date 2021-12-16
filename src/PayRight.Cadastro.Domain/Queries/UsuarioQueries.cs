using PayRight.Cadastro.Domain.Queries.DTOs;
using PayRight.Cadastro.Domain.Repositories;

namespace PayRight.Cadastro.Domain.Queries;

public class UsuarioQueries : IUsuarioQueries
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioQueries(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioInfoDTO?> BuscaInfoUsuario(Guid id)
    {
        var usuario = await _usuarioRepository.BuscaUsuario(id);

        if (usuario == null) return null;

        var usuarioInfo = new UsuarioInfoDTO()
        {
            Id = usuario.Id,
            NomeCompleto = $"{usuario.NomeCompleto.PrimeiroNome} {usuario.NomeCompleto.Sobrenome}",
            EnderecoEmail = usuario.NomeUsuario.Endereco,
            Ativo = usuario.Ativo,
            UltimaAtualizacaoEm = usuario.UltimaAtualizacaoEm
        };

        return usuarioInfo;
    }

    public async Task<UsuarioInfoCompletoDTO> BuscaUsuarioCompleto(Guid id)
    {
        var usuario = await _usuarioRepository.BuscaUsuarioCompleto(id);
        
        if (usuario == null) return null!;

        var usuarioDto = new UsuarioInfoCompletoDTO()
        {
            Id = usuario.Id,
            PrimeiroNome = usuario.NomeCompleto.PrimeiroNome,
            Sobrenome = usuario.NomeCompleto.Sobrenome,
            EnderecoEmail = usuario.NomeUsuario.Endereco,
            TipoDocumento = usuario.Documento.TipoDocumento,
            NumeroDocumento = usuario.Documento.Numero,
            Ativo = usuario.Ativo,
            CriadoEm = usuario.CriadoEm,
            UltimaAtualizacaoEm = usuario.UltimaAtualizacaoEm
        };

        return usuarioDto;
    }
}