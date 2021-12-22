using System;
using PayRight.Autenticacao.API.ValueObjects;
using Xunit;

namespace PayRight.Autenticacao.Tests.TestesUnitarios.ValueObjects.Fixtures;

[CollectionDefinition(nameof(TokenTestsCollection))]
public class TokenTestsCollection : ICollectionFixture<TokenTestsFixture>
{ }

public class TokenTestsFixture : IDisposable
{
    public Token GerarToken(string tokenValue = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IklTU08gRUggU08gVU0gVEVTVEUgU0FJIERBUVVJIiwiaWF0IjoxNTE2MjM5MDIyfQ.lsfIAVvTGJ-Fz5oakVkzcIvctP9dAXNvct8IDzD4F9c", DateTime validoAte = default)
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