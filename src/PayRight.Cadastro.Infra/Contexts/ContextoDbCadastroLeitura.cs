using Microsoft.EntityFrameworkCore;

namespace PayRight.Cadastro.Infra.Contexts;

public class ContextoDbCadastroLeitura : ContextoDbCoadastro
{
    public ContextoDbCadastroLeitura(DbContextOptions<ContextoDbCadastroLeitura> options) : base(options)
    { }
}