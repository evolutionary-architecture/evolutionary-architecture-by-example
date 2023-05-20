namespace EvolutionaryArchitecture.Fitnet.ReusableElements.SystemClock;

public interface ISystemClock
{
    DateTimeOffset Now { get; }
}