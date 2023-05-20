namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.SystemClock;

internal sealed class SystemClock : ISystemClock
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}