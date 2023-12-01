namespace EvolutionaryArchitecture.Fitnet.Common.Core.SystemClock;

using Microsoft.Extensions.DependencyInjection;

public static class SystemClockModule
{
    public static IServiceCollection AddSystemClock(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);

        return services;
    }
}
