using PayRight.Autenticacao.API.Repositories;
using PayRight.Autenticacao.API.Services;

namespace PayRight.Autenticacao.API.Configurations;

public static class InjecaoDeDependenciaConfiguration
{
    public static IServiceCollection ResolverDependencias(this IServiceCollection service)
    {
        service.AddScoped<IUsuarioAutenticacaoRepository, UsuarioAutenticacaoRepository>();

        service.AddScoped<IAutenticacaoService, AutenticacaoService>();
        
        return service;
    }
}