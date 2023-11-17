namespace EvolutionaryArchitecture.Fitnet.Offers.Prepare;

using Data;
using Data.Database;
using Passes.MarkPassAsExpired.Events;
using Common.Events;
using Common.Events.EventBus;
using Common.SystemClock;

internal sealed class PassExpiredEventHandler(
    IEventBus eventBus,
    OffersPersistence persistence,
    ISystemClock systemClock) : IIntegrationEventHandler<PassExpiredEvent>
{
    public async Task Handle(PassExpiredEvent @event, CancellationToken cancellationToken)
    {
        var offer = Offer.PrepareStandardPassExtension(@event.CustomerId, systemClock.Now);
        persistence.Offers.Add(offer);
        await persistence.SaveChangesAsync(cancellationToken);

        var offerPreparedEvent = OfferPrepareEvent.Create(offer.Id, offer.CustomerId);
        await eventBus.PublishAsync(offerPreparedEvent, cancellationToken);
    }
}
