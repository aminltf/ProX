using Microsoft.Extensions.DependencyInjection;

namespace Shared;

public static class DependencyInjection
{
    public static IServiceCollection SharedServices(this IServiceCollection services)
    {
        return services;
    }
}
