namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.SystemClock;

public interface ISystemClock
{
    DateTimeOffset Now { get; }
}