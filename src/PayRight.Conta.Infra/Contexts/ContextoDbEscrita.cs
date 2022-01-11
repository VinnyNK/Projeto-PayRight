using Microsoft.EntityFrameworkCore;

namespace PayRight.Conta.Infra.Contexts;

public class ContextoDbEscrita : ContextoDb
{
    public ContextoDbEscrita(DbContextOptions<ContextoDbEscrita> options) : base(options)
    {
    }
}