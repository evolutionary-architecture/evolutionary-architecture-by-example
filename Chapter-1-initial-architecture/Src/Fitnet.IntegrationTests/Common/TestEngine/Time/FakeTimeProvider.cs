namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Common.TestEngine.Time;

using Bogus;

[UsedImplicitly]
public sealed class FakeTimeProvider(DateTimeOffset? now = null) : TimeProvider
{
    private DateTimeOffset TimeNowOffset { get; set; } = now ?? new Faker().Date.RecentOffset().UtcDateTime;

    public override DateTimeOffset GetUtcNow() => TimeNowOffset;
}
