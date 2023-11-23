namespace EvolutionaryArchitecture.Fitnet.Offers.Api.Prepare;

using Common.Core.SystemClock;
using DataAccess;
using DataAccess.Database;
using MassTransit;
using Passes.IntegrationEvents;

internal sealed class PassExpiredEventConsumer(
    IPublishEndpoint eventBus,
    OffersPersistence persistence,
    ISystemClock systemClock) : IConsumer<PassExpiredEvent>
{
    public async Task Consume(ConsumeContext<PassExpiredEvent> context)
    {
        var @event = context.Message;
        var offer = Offer.PrepareStandardPassExtension(@event.CustomerId, systemClock.Now);
        persistence.Offers.Add(offer);
        await persistence.SaveChangesAsync(context.CancellationToken);

        var offerPreparedEvent = OfferPrepareEvent.Create(offer.Id, offer.CustomerId);
        await eventBus.Publish(offerPreparedEvent, context.CancellationToken);
    }
}
