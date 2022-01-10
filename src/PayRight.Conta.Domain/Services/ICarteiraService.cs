using PayRight.Conta.Domain.Models;

namespace PayRight.Conta.Domain.Services;

public interface ICarteiraService
{
   Task<bool> CriarCarteiraNovoUsuarioCriado(UsuarioMessage usuario);
}