using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace PayRight.Autenticacao.API.Configurations;

public static class AutenticacaoConfiguration
{
    public static IServiceCollection AddAutenticacao(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>  
        {  
            options.SaveToken = true;  
            options.RequireHttpsMetadata = false;  
            options.TokenValidationParameters = new TokenValidationParameters()  
            {  
                ValidateIssuer = true,  
                ValidateAudience = true,  
                ValidAudience = configuration["JWT:ValidAudience"],  
                ValidIssuer = configuration["JWT:ValidIssuer"],  
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                NameClaimType = configuration["JWT:ApplicationName"]
            };  
        });
        
        return services;
    }

    public static WebApplication ConfigureAutenticacao(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        return app;
    }
}