namespace EvolutionaryArchitecture.Fitnet.Common.SystemClock;

internal interface ISystemClock
{
    DateTimeOffset Now { get; }
}