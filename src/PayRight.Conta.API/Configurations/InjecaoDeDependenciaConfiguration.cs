using MediatR;
using PayRight.Conta.Domain.Queries;
using PayRight.Conta.Domain.Repositories;
using PayRight.Conta.Infra.Repositories;
using PayRight.Shared.Mediator;

namespace PayRight.Conta.API.Configurations;

public static class InjecaoDeDependenciaConfiguration
{
    public static IServiceCollection ResolverDependencias(this IServiceCollection service)
    {
        // Repositories
        service.AddScoped<IContaCorrenteEscritaRepository, ContaCorrenteEscritaRepository>();
        service.AddScoped<IContaCorrenteLeituraRepository, ContaCorrenteLeituraRepository>();
        
        // Queries
        service.AddScoped<IContaCorrenteQueries, ContaCorrenteQueries>();

        // Mediatr
        service.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        service.AddScoped<IMediatorHandler, MediatorHandler>();

        return service;
    }
}