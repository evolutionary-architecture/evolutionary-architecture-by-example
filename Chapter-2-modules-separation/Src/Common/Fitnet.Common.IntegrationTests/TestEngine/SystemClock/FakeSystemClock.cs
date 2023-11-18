namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.SystemClock;

using Core.SystemClock;

internal sealed class FakeSystemClock(DateTimeOffset fakeDateTimeOffset) : ISystemClock
{
    public DateTimeOffset Now => fakeDateTimeOffset;
}
