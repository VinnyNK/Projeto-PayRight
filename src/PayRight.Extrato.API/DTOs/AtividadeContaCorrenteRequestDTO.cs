using System.Globalization;
using PayRight.Extrato.Domain.Enums;

namespace PayRight.Extrato.API.DTOs;

public class AtividadeContaCorrenteRequestDTO
{
    public Guid UsuarioId { get; set; }

    public Guid ContaCorrenteId { get; set; }

    public string? NomeAtividade { get; set; }

    public string? DescricaoAtividade { get; set; }

    public decimal Valor { get; set; }

    public TipoAtividade TipoAtividade { get; set; }

    public DateTime DataEstimada { get; set; }
}