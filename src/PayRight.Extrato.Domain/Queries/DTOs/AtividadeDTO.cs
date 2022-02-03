using PayRight.Extrato.Domain.Enums;

namespace PayRight.Extrato.Domain.Queries.DTOs;

public class AtividadeDTO
{
    public Guid Id { get; set; }

    public string NomeAtividade { get; set; }

    public string? DescricaoAtividade { get; set; }

    public decimal Valor { get; set; }

    public string TipoAtividade { get; set; }

    public bool Pago { get; set; }

    public DateOnly? DataPagamento { get; set; }

    public DateOnly EstimativaPagamento { get; set; }
}