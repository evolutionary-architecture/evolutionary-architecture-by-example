namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Common.TestEngine.SystemClock;

using ReusableElements.SystemClock;

internal sealed class FakeSystemClock : ISystemClock
{
    public FakeSystemClock(DateTimeOffset fakeDateTimeOffset) => Now = fakeDateTimeOffset;

    public DateTimeOffset Now { get; }
}