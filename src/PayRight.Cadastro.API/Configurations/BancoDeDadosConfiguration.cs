using Microsoft.EntityFrameworkCore;
using PayRight.Cadastro.Infra.Contexts;

namespace PayRight.Cadastro.API.Configurations;

public static class BancoDeDadosConfiguration
{
    public static IServiceCollection InjecaoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var serverVersion = new MySqlServerVersion(new Version(10, 6, 5));
        
        services.AddDbContext<ContextoDbCadastroLeitura>(_ => _.UseMySql(configuration.GetConnectionString("DbConnectionLeitura"), serverVersion));
        
        services.AddDbContext<ContextoDbCadastroEscrita>(_ => _.UseMySql(configuration.GetConnectionString("DbConnectionEscrita"), serverVersion));

        return services;
    }
}