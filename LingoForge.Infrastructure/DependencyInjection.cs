using LingoForge.Domain.Interfaces.Repositories;
using LingoForge.Domain.Security.Cryptography;
using LingoForge.Domain.Security.Tokens;
using LingoForge.Infrastructure.DataAccess;
using LingoForge.Infrastructure.DataAccess.Repositories;
using LingoForge.Infrastructure.Security.Cryptography;
using LingoForge.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LingoForge.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDBContext(services, configuration);
        AddRepositories(services);
        AddToken(services, configuration);

        services.AddScoped<IPasswordEncryption, PasswordEncryption>();
    }

    public static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IJwtTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }

    private static void AddDBContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LingoForgeDbContext>(x => x.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnityOfWork, UnityOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITurmaRepository, TurmaRepository>();
        services.AddScoped<IActivityRepository, ActivityRepository>();
    }
}
