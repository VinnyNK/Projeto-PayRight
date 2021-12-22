using System;
using PayRight.Autenticacao.API.ValueObjects;
using Xunit;

namespace PayRight.Autenticacao.Tests.TestesUnitarios.ValueObjects.Fixtures;

[CollectionDefinition(nameof(TokenTestsCollection))]
public class TokenTestsCollection : ICollectionFixture<TokenTestsFixture>
{ }

public class TokenTestsFixture : IDisposable
{
    public Token GerarToken(string tokenValue = "asasssss.gdfsaddfasdasd.ssdasdasdas-sdadasdasdasd", DateTime validoAte = default)
    {
        if (validoAte == default)
            validoAte = DateTime.Now.AddDays(30);
        
        var token = new Token(tokenValue, validoAte);

        return token;
    }
    
    
    public void Dispose()
    {
    }
}