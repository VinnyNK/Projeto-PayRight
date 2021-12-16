using System;
using PayRight.Cadastro.Domain.Enums;
using PayRight.Cadastro.Domain.ValueObjects;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.Entities.Fixtures;

[CollectionDefinition(nameof(UsuarioCollection))]
public class UsuarioCollection : ICollectionFixture<UsuarioTestsFixture>
{ }

public class UsuarioTestsFixture : IDisposable
{
    public Domain.Entities.Usuario GerarNovoUsuario(string primeiroNome = "Fulano", string sobrenome = "da Silva", string numeroDocumento = "03382042029", TipoDocumento tipoDocumento = TipoDocumento.CPF, string enderecoEmail = "fulana@example.com", string senha = "Abc123@def",
        string confirmacaoSenha = "Abc123@def")
    {
        var nome = new NomeCompleto(primeiroNome, sobrenome);
        var documento = new Documento(numeroDocumento, tipoDocumento);
        var email = new Email(enderecoEmail);
        var usuario = new Domain.Entities.Usuario(nome, email, documento, senha, confirmacaoSenha);

        return usuario;
    }
    
    
    public void Dispose()
    {
    }
}