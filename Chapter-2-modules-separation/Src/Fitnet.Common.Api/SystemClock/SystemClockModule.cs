using Microsoft.Extensions.DependencyInjection;

namespace EvolutionaryArchitecture.Fitnet.Common.Api.SystemClock;

public static class SystemClockModule
{
    public static IServiceCollection AddSystemClock(this IServiceCollection services)
    {
        services.AddSingleton<ISystemClock, SystemClock>();

        return services;
    }
}