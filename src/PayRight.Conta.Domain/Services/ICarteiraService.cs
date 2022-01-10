using PayRight.Conta.Domain.Messages;

namespace PayRight.Conta.Domain.Services;

public interface ICarteiraService
{
   Task<bool> CriarCarteiraNovoUsuarioCriado(UsuarioMessage usuario);
}