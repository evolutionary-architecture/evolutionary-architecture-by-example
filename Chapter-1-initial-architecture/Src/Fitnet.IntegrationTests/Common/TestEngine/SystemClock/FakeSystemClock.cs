namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Common.TestEngine.SystemClock;

using EvolutionaryArchitecture.Fitnet.Shared.SystemClock;

internal sealed class FakeSystemClock : ISystemClock
{
    public FakeSystemClock(DateTimeOffset fakeDateTimeOffset) => Now = fakeDateTimeOffset;

    public DateTimeOffset Now { get; }
}