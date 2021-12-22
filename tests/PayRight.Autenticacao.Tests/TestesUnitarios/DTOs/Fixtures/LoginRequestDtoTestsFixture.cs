using System;
using PayRight.Autenticacao.API.DTOs;
using Xunit;

namespace PayRight.Autenticacao.Tests.TestesUnitarios.DTOs.Fixtures;

[CollectionDefinition(nameof(LoginRequestDtoTestCollection))]
public class LoginRequestDtoTestCollection : ICollectionFixture<LoginRequestDtoTestsFixture>
{ }

public class LoginRequestDtoTestsFixture : IDisposable
{
    public LoginDto GerarNovoLogin(string email = "email@example.com", string senha = "SenhaSegura")
    {
        var login = new LoginDto()
        {
            Email = email,
            Senha = senha
        };

        return login;
    }
    
    
    public void Dispose()
    {
    }
}