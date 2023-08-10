namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.SystemClock;

using Core.SystemClock;

internal sealed class FakeSystemClock : ISystemClock
{
    public FakeSystemClock(DateTimeOffset fakeDateTimeOffset) => Now = fakeDateTimeOffset;

    public DateTimeOffset Now { get; }
}