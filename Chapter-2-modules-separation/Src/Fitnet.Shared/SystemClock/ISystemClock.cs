namespace EvolutionaryArchitecture.Fitnet.Shared.SystemClock;

public interface ISystemClock
{
    DateTimeOffset Now { get; }
}