using Microsoft.EntityFrameworkCore;

namespace PayRight.Extrato.Infra.Contexts;

public class ContextoDbLeitura : ContextoDb
{
    public ContextoDbLeitura(DbContextOptions<ContextoDbLeitura> options) : base(options)
    {
    }
}