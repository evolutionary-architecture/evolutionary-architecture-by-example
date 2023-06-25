namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.SystemClock;

using Core.SystemClock;

internal sealed class FakeSystemClock : ISystemClock
{
    public FakeSystemClock(DateTimeOffset fakeDateTimeOffset) => Now = fakeDateTimeOffset;

    public DateTimeOffset Now { get; }
}