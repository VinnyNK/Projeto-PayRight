using MediatR;
using PayRight.Cadastro.Domain.Queries;
using PayRight.Cadastro.Domain.Repositories;
using PayRight.Cadastro.Infra.Repositories;
using PayRight.Shared.Mediator;

namespace PayRight.Cadastro.API.Configurations;

public static class InjecaoDeDependenciaConfiguration
{
    public static IServiceCollection ResolverDependencias(this IServiceCollection service)
    {
        // Repositories
        service.AddScoped<IUsuarioLeituraRepository, UsuarioLeituraRepository>();
        service.AddScoped<IUsuarioEscritaRepository, UsuarioEscritaRepository>();
        
        // Queries
        service.AddScoped<IUsuarioQueries, UsuarioQueries>();
        
        // Mediatr
        service.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        service.AddScoped<IMediatorHandler, MediatorHandler>();

        return service;
    }
}