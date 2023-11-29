namespace EvolutionaryArchitecture.Fitnet.Common.Clock;

internal static class ClockModule
{
    internal static IServiceCollection AddClock(this IServiceCollection services) =>
        services.AddSingleton(TimeProvider.System);
}
