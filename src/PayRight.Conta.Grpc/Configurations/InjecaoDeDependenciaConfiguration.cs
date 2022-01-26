using PayRight.Conta.Grpc.Repositories;

namespace PayRight.Conta.Grpc.Configurations;

public static class InjecaoDeDependenciaConfiguration
{
    public static IServiceCollection ResolverDependencias(this IServiceCollection service)
    {
        // Repositories
        service.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
        
        return service;
    }
}