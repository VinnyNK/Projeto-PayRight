using PayRight.Cadastro.Domain.Enums;

namespace PayRight.Cadastro.Domain.Queries.DTOs;

public class UsuarioInfoCompletoDTO
{
    public Guid Id { get; set; }

    public string? PrimeiroNome { get; set; }
    
    public string? Sobrenome { get; set; }

    public string? EnderecoEmail { get; set; }

    public TipoDocumento TipoDocumento { get; set; }

    public string? NumeroDocumento { get; set; }

    public bool Ativo { get; set; }
    
    public DateTime CriadoEm { get; set; }

    public DateTime UltimaAtualizacaoEm { get; set; }
}