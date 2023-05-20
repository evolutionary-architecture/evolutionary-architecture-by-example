namespace EvolutionaryArchitecture.Fitnet.Shared.SystemClock;

internal sealed class SystemClock : ISystemClock
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}