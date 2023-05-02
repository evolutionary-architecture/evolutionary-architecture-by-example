namespace SuperSimpleArchitecture.Fitnet.Shared.SystemClock;

internal interface ISystemClock
{
    DateTimeOffset Now { get; }
}