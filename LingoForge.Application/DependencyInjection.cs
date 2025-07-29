using LingoForge.Application.UseCases.Auth;
using LingoForge.Domain.Interfaces.UseCases.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace LingoForge.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ICreateAccountUseCase, CreateAccountUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
    }
}
