namespace EvolutionaryArchitecture.Fitnet.Offers.Api.Prepare;

using Common.Infrastructure.Events;
using Common.Infrastructure.Events.EventBus;
using DataAccess;
using DataAccess.Database;
using Passes.IntegrationEvents;

internal sealed class PassExpiredEventHandler(
    IEventBus eventBus,
    OffersPersistence persistence,
    TimeProvider timeProvider) : IIntegrationEventHandler<PassExpiredEvent>
{
    public async Task Handle(PassExpiredEvent @event, CancellationToken cancellationToken)
    {
        var nowDate = timeProvider.GetUtcNow();
        var offer = Offer.PrepareStandardPassExtension(@event.CustomerId, nowDate);
        await persistence.Offers.AddAsync(offer, cancellationToken);
        await persistence.SaveChangesAsync(cancellationToken);

        var offerPreparedEvent = OfferPrepareEvent.Create(offer.Id, offer.CustomerId, timeProvider.GetUtcNow());
        await eventBus.PublishAsync(offerPreparedEvent, cancellationToken);
    }
}
