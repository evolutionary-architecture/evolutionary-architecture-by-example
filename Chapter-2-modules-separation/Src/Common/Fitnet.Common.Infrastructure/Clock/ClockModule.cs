namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Clock;

using Microsoft.Extensions.DependencyInjection;

internal static class ClockModule
{
    internal static IServiceCollection AddClock(this IServiceCollection services) =>
        services.AddSingleton(TimeProvider.System);
}
