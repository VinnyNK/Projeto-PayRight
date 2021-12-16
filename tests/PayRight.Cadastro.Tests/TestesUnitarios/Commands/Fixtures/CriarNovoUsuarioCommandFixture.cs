using System;
using PayRight.Cadastro.Domain.Commands;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.Commands.Fixtures;
          
[CollectionDefinition(nameof(CriarNovoUsuarioCommandCollection))]
public class CriarNovoUsuarioCommandCollection : ICollectionFixture<CriarNovoUsuarioCommandFixture>
{ }

public class CriarNovoUsuarioCommandFixture : IDisposable
{
    public CriarNovoUsuarioCpfCommand GerarNovoUsuarioCpfCommand(string primeiroNome = "Laysla", 
        string sobrenome = "Esparteiro Freitas", 
        string enderecoEmail = "laysla.freitas@gmail.com", 
        string numeroDocumento = "30755386043", 
        string senha = "12QWaszx.", 
        string confirmacaoSenha = "12QWaszx.")
    {
        return new CriarNovoUsuarioCpfCommand(primeiroNome, sobrenome, enderecoEmail, numeroDocumento, senha,
            confirmacaoSenha);
    }
    
    public CriarNovoUsuarioCnpjCommand GerarNovoUsuarioCnpjCommand(string primeiroNome = "Laysla", 
        string sobrenome = "Esparteiro Freitas", 
        string enderecoEmail = "laysla.freitas@gmail.com", 
        string numeroDocumento = "93586993000106", 
        string senha = "12QWaszx.", 
        string confirmacaoSenha = "12QWaszx.")
    {
        return new CriarNovoUsuarioCnpjCommand(primeiroNome, sobrenome, enderecoEmail, numeroDocumento, senha,
            confirmacaoSenha);
    }

    public void Dispose()
    {
    }
}