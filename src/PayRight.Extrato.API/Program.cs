using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using PayRight.Extrato.API.Configurations;
using PayRight.Shared.Utils.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddAutenticacao(builder.Configuration);

builder.Services.SwaggerService();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.InjecaoDb(builder.Configuration);

builder.Services.AddMvc(_ => _.Filters.Add(typeof(ValidationFilter)))
    .AddFluentValidation(_ =>
    {
        _.RegisterValidatorsFromAssemblyContaining<Program>();
        _.ImplicitlyValidateChildProperties = true;
    });

builder.Services.Configure<ApiBehaviorOptions>(_ => _.SuppressModelStateInvalidFilter = true);

builder.Services.ResolverDependencias();

var app = builder.Build();

app.SwaggerConfigure();

app.UseHttpsRedirection();

app.ConfigureAutenticacao();

app.MapControllers();

app.Run();