namespace PayRight.Conta.API.DTOs;

public class ListarContasCorrentesResponse
{
    public Guid Id { get; set; }

    public bool Ativo { get; set; }

    public string? Nome { get; set; }

    public string? Apelido { get; set; }

    public decimal? Saldo { get; set; }
}