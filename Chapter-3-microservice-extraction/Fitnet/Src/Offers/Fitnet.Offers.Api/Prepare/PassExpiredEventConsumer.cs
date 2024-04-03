namespace EvolutionaryArchitecture.Fitnet.Offers.Api.Prepare;

using DataAccess;
using DataAccess.Database;
using MassTransit;
using Passes.IntegrationEvents;

internal sealed class PassExpiredEventConsumer(
    IPublishEndpoint eventBus,
    OffersPersistence persistence,
    TimeProvider timeProvider) : IConsumer<PassExpiredEvent>
{
    public async Task Consume(ConsumeContext<PassExpiredEvent> context)
    {
        var @event = context.Message;
        var offer = Offer.PrepareStandardPassExtension(@event.CustomerId, timeProvider.GetUtcNow());
        persistence.Offers.Add(offer);
        await persistence.SaveChangesAsync(context.CancellationToken);

        var offerPreparedEvent = OfferPrepareEvent.Create(offer.Id, offer.CustomerId, timeProvider.GetUtcNow());
        await eventBus.Publish(offerPreparedEvent, context.CancellationToken);
    }
}
