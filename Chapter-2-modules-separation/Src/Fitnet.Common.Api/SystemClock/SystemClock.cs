namespace EvolutionaryArchitecture.Fitnet.Common.Api.SystemClock;

internal sealed class SystemClock : ISystemClock
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}