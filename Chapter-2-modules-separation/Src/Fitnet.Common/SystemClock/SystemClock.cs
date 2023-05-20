namespace EvolutionaryArchitecture.Fitnet.ReusableElements.SystemClock;

internal sealed class SystemClock : ISystemClock
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}