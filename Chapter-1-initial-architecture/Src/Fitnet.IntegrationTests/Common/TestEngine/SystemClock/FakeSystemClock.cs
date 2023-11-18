namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Common.TestEngine.SystemClock;

using EvolutionaryArchitecture.Fitnet.Common.SystemClock;

internal sealed class FakeSystemClock(DateTimeOffset fakeDateTimeOffset) : ISystemClock
{
    public DateTimeOffset Now { get; } = fakeDateTimeOffset;
}
