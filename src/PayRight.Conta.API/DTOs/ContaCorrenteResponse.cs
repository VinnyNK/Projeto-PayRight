namespace PayRight.Conta.API.DTOs;

public class ContaCorrenteResponse
{
    public Guid Id { get; set; }

    public bool Ativo { get; set; }

    public string? Nome { get; set; }

    public string? Apelido { get; set; }

    public decimal? Saldo { get; set; }

    public DateTime CriadoEm { get; set; }

    public DateTime UltimaAtualizacaoEm { get; set; }
}