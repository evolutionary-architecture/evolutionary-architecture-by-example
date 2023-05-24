using EvolutionaryArchitecture.Fitnet.Passes.IntegrationEvents;

namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Offers.Prepare;

internal sealed class PassExpiredEventFaker : Faker<PassExpiredEvent>
{
    private PassExpiredEventFaker()
    {
        CustomInstantiator(faker =>
            new PassExpiredEvent(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                faker.Date.RecentOffset()
            )
        );
    }
    
    internal static PassExpiredEvent CreateValid() => new PassExpiredEventFaker();
}