using System;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.ValueObjects.Fixtures;

[CollectionDefinition(nameof(EmailCollection))]
public class EmailCollection : ICollectionFixture<EmailFixture>
{ }

public class EmailFixture : IDisposable
{
    public Domain.ValueObjects.Email GerarEmail(string endereco = "email@example.com")
    {
        return new Domain.ValueObjects.Email(endereco);
    }
    
    public void Dispose()
    {
    }
}