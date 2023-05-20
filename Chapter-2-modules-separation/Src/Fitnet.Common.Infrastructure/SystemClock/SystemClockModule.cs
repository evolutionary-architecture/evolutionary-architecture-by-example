namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.SystemClock;

using Microsoft.Extensions.DependencyInjection;

public static class SystemClockModule
{
    public static IServiceCollection AddSystemClock(this IServiceCollection services)
    {
        services.AddSingleton<ISystemClock, SystemClock>();

        return services;
    }
}