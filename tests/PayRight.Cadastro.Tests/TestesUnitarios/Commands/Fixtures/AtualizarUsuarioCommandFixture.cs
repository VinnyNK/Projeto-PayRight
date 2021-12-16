using System;
using PayRight.Cadastro.Domain.Commands;
using PayRight.Cadastro.Domain.Entities;
using PayRight.Cadastro.Tests.TestesUnitarios.Entities.Fixtures;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.Commands.Fixtures;
          
[CollectionDefinition(nameof(AtualizarUsuarioCommandCollection))] 
public class AtualizarUsuarioCommandCollection : ICollectionFixture<AtualizarUsuarioCommandFixture>, ICollectionFixture<UsuarioTestsFixture>
{ }
          
public class AtualizarUsuarioCommandFixture : IDisposable
{
    public AtualizarUsuarioCommand GerarAtualizarUsuarioCommand(string primeiroNome = "Laysla", string sobrenome = "Esparteiro Freitas", string enderecoEmail = "laysla.freitas@gmail.com") 
    { 
        var criarUsuarioFixture = new UsuarioTestsFixture(); 
        var usuario = criarUsuarioFixture.GerarNovoUsuario(); 
        return new AtualizarUsuarioCommand(usuario.Id, primeiroNome, sobrenome, enderecoEmail); 
    }
    
    public AtualizarUsuarioCommand GerarAtualizarUsuarioCommand(Usuario usuario, string primeiroNome = "Laysla", string sobrenome = "Esparteiro Freitas", string enderecoEmail = "laysla.freitas@gmail.com") 
    { 
        var criarUsuarioFixture = new UsuarioTestsFixture();
        return new AtualizarUsuarioCommand(usuario.Id, primeiroNome, sobrenome, enderecoEmail); 
    }
    
    public void Dispose() { } 
}