using Microsoft.EntityFrameworkCore;

namespace PayRight.Cadastro.Infra.Contexts;

public class ContextoDbCadastroEscrita : ContextoDbCoadastro
{
    public ContextoDbCadastroEscrita(DbContextOptions<ContextoDbCadastroEscrita> options) : base(options)
    { }
}