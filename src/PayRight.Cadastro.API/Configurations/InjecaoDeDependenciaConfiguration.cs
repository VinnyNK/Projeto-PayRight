using MediatR;
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
        
        // Mediatr
        service.AddMediatR(typeof(Program));
        service.AddScoped<IMediatorHandler, MediatorHandler>();

        return service;
    }
}