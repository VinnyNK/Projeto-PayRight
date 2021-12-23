using Microsoft.EntityFrameworkCore;
using PayRight.Autenticacao.API.Context;

namespace PayRight.Autenticacao.API.Configurations;

public static class BancoDeDadosConfiguration
{
    public static IServiceCollection InjecaoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var serverVersion = new MySqlServerVersion(new Version(10, 6, 5));

        services.AddDbContext<ContextoDbLeitura>(_ => _.UseMySql(configuration.GetConnectionString("DbConnection"), serverVersion));
        
        return services;
    }
}