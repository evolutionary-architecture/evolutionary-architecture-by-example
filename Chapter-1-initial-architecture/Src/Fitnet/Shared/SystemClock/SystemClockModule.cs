namespace EvolutionaryArchitecture.Fitnet.Shared.SystemClock;

internal static class SystemClockModule
{
    internal static IServiceCollection AddSystemClock(this IServiceCollection services)
    {
        services.AddSingleton<ISystemClock, SystemClock>();

        return services;
    }
}