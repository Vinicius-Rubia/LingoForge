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

    }
}
