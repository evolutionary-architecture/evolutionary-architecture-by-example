namespace EvolutionaryArchitecture.Fitnet.Offers.Api.Prepare;

using Common.Core.SystemClock;
using Common.Infrastructure.Events;
using Common.Infrastructure.Events.EventBus;
using DataAccess;
using DataAccess.Database;
using Passes.IntegrationEvents;

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
