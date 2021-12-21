namespace PayRight.Cadastro.API.Configurations;

public static class SwaggerConfiguration
{
    public static IServiceCollection SwaggerService(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }

    public static WebApplication SwaggerConfigure(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return app;
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}