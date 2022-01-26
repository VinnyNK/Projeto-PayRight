using PayRight.Conta.Grpc.Protos;
using PayRight.Extrato.API.GrpcService;
using PayRight.Extrato.Domain.GrpcService;

namespace PayRight.Extrato.API.Configurations;

public static class GrpcConfiguration
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<ContaCorrenteProtoService.ContaCorrenteProtoServiceClient>(_ =>
            _.Address = new Uri(configuration["Grpc:Conta"]));
        
        services.AddScoped<IContaCorrenteGrpcService, ContaCorrenteGrpcService>();

        return services;
    }
}