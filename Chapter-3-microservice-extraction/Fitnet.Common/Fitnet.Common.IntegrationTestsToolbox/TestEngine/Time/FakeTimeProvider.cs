namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.Time;

[UsedImplicitly]
public sealed class FakeTimeProvider(DateTimeOffset? now = null) : TimeProvider
{
    private DateTimeOffset TimeNowOffset { get; set; } = now ?? new Faker().Date.RecentOffset().UtcDateTime;

    public override DateTimeOffset GetUtcNow() => TimeNowOffset;
}
