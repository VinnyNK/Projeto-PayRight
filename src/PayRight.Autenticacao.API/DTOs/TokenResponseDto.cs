namespace PayRight.Autenticacao.API.DTOs;

public class TokenResponseDto
{
    public string? Token { get; set; }

    public DateTime ValidoAte { get; set; }
}