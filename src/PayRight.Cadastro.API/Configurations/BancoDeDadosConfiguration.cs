using Microsoft.EntityFrameworkCore;
using PayRight.Cadastro.Infra.Contexts;

namespace PayRight.Cadastro.API.Configurations;

public static class BancoDeDadosConfiguration
{
    public static IServiceCollection InjecaoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ContextoDbCadastroLeitura>(_ => _.UseMySQL(configuration.GetConnectionString("DbConnectionLeitura")));
        
        services.AddDbContext<ContextoDbCadastroEscrita>(_ => _.UseMySQL(configuration.GetConnectionString("DbConnectionEscrita")));

        return services;
    }
}