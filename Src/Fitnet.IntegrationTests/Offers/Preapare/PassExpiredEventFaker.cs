namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Offers.Preapare;

using SuperSimpleArchitecture.Fitnet.Passes.MarkPassAsExpired.Events;

internal sealed class PassExpiredEventFaker : Faker<PassExpiredEvent>
{
    private PassExpiredEventFaker()
    {
        CustomInstantiator(faker =>
            new PassExpiredEvent(
                Guid.NewGuid(),
                Guid.NewGuid(),
                faker.Date.RecentOffset()
            )
        );
    }
    
    internal static PassExpiredEvent CreateValid() => new PassExpiredEventFaker();
}