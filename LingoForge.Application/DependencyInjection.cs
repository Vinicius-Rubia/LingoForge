using LingoForge.Application.UseCases.Auth;
using LingoForge.Application.UseCases.Turmas;
using LingoForge.Application.UseCases.Users;
using LingoForge.Domain.Interfaces.UseCases.Auth;
using LingoForge.Domain.Interfaces.UseCases.Turmas;
using LingoForge.Domain.Interfaces.UseCases.Users;
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
        services.AddScoped<IDeleteStudentAccountUseCase, DeleteStudentAccountUseCase>();
        services.AddScoped<ICreateClassUseCase, CreateClassUseCase>();
    }
}
