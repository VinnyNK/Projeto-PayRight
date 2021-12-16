namespace PayRight.Cadastro.Domain.Queries.DTOs;

public class UsuarioInfoDTO
{
    public Guid Id { get; set; }

    public string NomeCompleto { get; set; }

    public string EnderecoEmail { get; set; }

    public bool Ativo { get; set; }
    
    public DateTime UltimaAtualizacaoEm { get; set; }
}