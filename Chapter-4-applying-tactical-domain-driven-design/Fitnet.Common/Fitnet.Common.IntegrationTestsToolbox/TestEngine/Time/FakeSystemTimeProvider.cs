namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.SystemClock;

using Bogus;

[UsedImplicitly]
public sealed class FakeSystemTimeProvider : TimeProvider
{
    private DateTimeOffset TimeNowOffset { get; set; } = new Faker().Date.RecentOffset().UtcDateTime;

    public override DateTimeOffset GetUtcNow() => TimeNowOffset;

    public void SimulateTimeSkip(int passedDays = 30) =>
        TimeNowOffset = TimeNowOffset.AddDays(passedDays);
}
