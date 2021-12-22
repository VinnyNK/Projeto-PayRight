using Flunt.Notifications;
using PayRight.Autenticacao.API.Models;
using PayRight.Autenticacao.API.ValueObjects;
using PayRight.Shared.Services;

namespace PayRight.Autenticacao.API.Services;

public interface IAutenticacaoService : IServiceBase
{
    Task<Token?>? Login(string email, string senha);
}