namespace EvolutionaryArchitecture.Fitnet.Common.Core.SystemClock;

internal sealed class SystemClock : ISystemClock
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}