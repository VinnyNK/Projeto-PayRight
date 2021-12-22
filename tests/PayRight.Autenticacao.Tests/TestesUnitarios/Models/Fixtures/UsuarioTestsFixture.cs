using System;
using PayRight.Autenticacao.API.Models;
using Xunit;

namespace PayRight.Autenticacao.Tests.TestesUnitarios.Models.Fixtures;

[CollectionDefinition(nameof(UsuarioCollection))]
public class UsuarioCollection : ICollectionFixture<UsuarioTestsFixture>
{ }

public class UsuarioTestsFixture : IDisposable
{
    public Usuario GerarNovoUsuario(string email = "email@example.com", string senha = "SenhaSegura")
    {
        var usuario = new Usuario(email, senha);
        
        return usuario;
    }
    
    
    public void Dispose()
    {
    }
}