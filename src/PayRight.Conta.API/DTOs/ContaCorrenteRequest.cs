namespace PayRight.Conta.API.DTOs;

public class ContaCorrenteRequest
{
    public Guid UsuarioId { get; set; }
    
    public string? Nome { get; set; }

    public string? Apelido { get; set; }

    public decimal SaldoInicial { get; set; }
}