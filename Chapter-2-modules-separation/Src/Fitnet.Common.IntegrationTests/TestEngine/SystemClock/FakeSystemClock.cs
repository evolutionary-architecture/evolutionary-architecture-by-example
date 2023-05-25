namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.SystemClock;

using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.SystemClock;

internal sealed class FakeSystemClock : ISystemClock
{
    public FakeSystemClock(DateTimeOffset fakeDateTimeOffset) => Now = fakeDateTimeOffset;

    public DateTimeOffset Now { get; }
}