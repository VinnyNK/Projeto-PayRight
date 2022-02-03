namespace PayRight.Extrato.Domain.Queries.DTOs;

public class ExtratoContaCorrenteDTO
{
    public Guid Id { get; set; }

    public Guid ContaCorrenteId { get; set; }

    public int Mes { get; set; }

    public int Ano { get; set; }

    public decimal Total { get; set; }

    public decimal TotalEstimado { get; set; }

    public IEnumerable<AtividadeDTO> Atividades { get; set; }
}