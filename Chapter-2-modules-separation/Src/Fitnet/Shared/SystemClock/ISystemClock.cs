namespace EvolutionaryArchitecture.Fitnet.Shared.SystemClock;

internal interface ISystemClock
{
    DateTimeOffset Now { get; }
}