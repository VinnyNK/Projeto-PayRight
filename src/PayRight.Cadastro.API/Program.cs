using PayRight.Cadastro.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.SwaggerService();

builder.Services.InjecaoDb(builder.Configuration);

builder.Services.ResolverDependencias();


var app = builder.Build();

app.SwaggerConfigure();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();