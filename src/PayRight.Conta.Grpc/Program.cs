using PayRight.Conta.Grpc.Configurations;
using PayRight.Conta.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.InjecaoDb(builder.Configuration);

builder.Services.ResolverDependencias();

var app = builder.Build();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.MapGrpcService<ContaCorrenteService>();

app.Run();