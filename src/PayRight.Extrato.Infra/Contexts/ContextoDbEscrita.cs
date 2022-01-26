using Microsoft.EntityFrameworkCore;

namespace PayRight.Extrato.Infra.Contexts;

public class ContextoDbEscrita : ContextoDb
{
    public ContextoDbEscrita(DbContextOptions<ContextoDbEscrita> options) : base(options)
    {
    }
}