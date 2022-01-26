using MediatR;
using PayRight.Extrato.Domain.UnitOfWork;
using PayRight.Extrato.Infra.UnitOfWork;
using PayRight.Shared.Mediator;

namespace PayRight.Extrato.API.Configurations;

public static class InjecaoDeDependenciaConfiguration
{
    public static IServiceCollection ResolverDependencias(this IServiceCollection service)
    {
        // Repositories
        service.AddScoped<IUnitOfWork, UnitOfWork>();

        // Queries
        

        // Mediatr
        service.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        service.AddScoped<IMediatorHandler, MediatorHandler>();

        return service;
    }
}