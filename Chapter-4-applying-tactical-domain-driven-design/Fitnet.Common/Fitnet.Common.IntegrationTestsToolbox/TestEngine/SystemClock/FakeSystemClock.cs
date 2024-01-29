namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.SystemClock;

using Core.SystemClock;

internal sealed class FakeSystemClock(DateTimeOffset fakeDateTimeOffset) : ISystemClock
{
    public DateTimeOffset Now { get; } = fakeDateTimeOffset;
}
