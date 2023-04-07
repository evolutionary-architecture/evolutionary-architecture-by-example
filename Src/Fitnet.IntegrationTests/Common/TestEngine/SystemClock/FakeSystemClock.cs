namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Common.TestEngine.SystemClock;

using SuperSimpleArchitecture.Fitnet.Shared.SystemClock;

internal sealed class FakeSystemClock : ISystemClock
{
    private readonly DateTimeOffset _fakeDateTimeOffset;

    public FakeSystemClock(DateTimeOffset fakeDateTimeOffset) => _fakeDateTimeOffset = fakeDateTimeOffset;
    
    DateTimeOffset ISystemClock.Now => _fakeDateTimeOffset;
}