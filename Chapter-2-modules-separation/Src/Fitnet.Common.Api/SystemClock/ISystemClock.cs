namespace EvolutionaryArchitecture.Fitnet.Common.Api.SystemClock;

public interface ISystemClock
{
    DateTimeOffset Now { get; }
}