using Microsoft.EntityFrameworkCore;
using PayRight.Extrato.Infra.Contexts;

namespace PayRight.Extrato.API.Configurations;

public static class BancoDeDadosConfiguration
{
    public static IServiceCollection InjecaoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var serverVersion = new MySqlServerVersion(new Version(10, 6, 5));
        
        services.AddDbContext<ContextoDbLeitura>(_ => _.UseMySql(configuration.GetConnectionString("DbConnectionLeitura"), serverVersion));
        
        services.AddDbContext<ContextoDbEscrita>(_ => _.UseMySql(configuration.GetConnectionString("DbConnectionEscrita"), serverVersion));

        return services;
    }
}