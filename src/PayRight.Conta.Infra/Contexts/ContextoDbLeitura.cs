using Microsoft.EntityFrameworkCore;

namespace PayRight.Conta.Infra.Contexts;

public class ContextoDbLeitura : ContextoDb
{
    public ContextoDbLeitura(DbContextOptions<ContextoDbLeitura> options) : base(options)
    {
    }
}