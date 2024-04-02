namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Time;

[UsedImplicitly]
public sealed class FakeTimeProvider : TimeProvider
{
    private DateTimeOffset TimeNowOffset { get; set; } = new Faker().Date.RecentOffset().UtcDateTime;

    public override DateTimeOffset GetUtcNow() => TimeNowOffset;

    public void SimulateTimeSkip(int passedDays = 30) =>
        TimeNowOffset = TimeNowOffset.AddDays(passedDays);
}
