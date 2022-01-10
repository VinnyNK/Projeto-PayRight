namespace PayRight.Conta.Domain.Queries.DTOs;

public class ContaCorrenteDTO
{
    public Guid Id { get; set; }

    public bool Ativo { get; set; }
    public string Nome { get; set; }

    public string? Apelido { get; set; }

    public decimal Saldo { get; set; }
}