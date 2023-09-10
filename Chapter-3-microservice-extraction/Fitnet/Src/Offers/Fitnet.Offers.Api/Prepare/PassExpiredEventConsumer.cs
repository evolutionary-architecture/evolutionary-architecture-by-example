namespace EvolutionaryArchitecture.Fitnet.Offers.Api.Prepare;

using Common.Core.SystemClock;
using Common.Infrastructure.Events.EventBus;
using DataAccess;
using DataAccess.Database;
using MassTransit;
using Passes.IntegrationEvents;

internal sealed class PassExpiredEventConsumer : IConsumer<PassExpiredEvent>
{
    private readonly IEventBus _eventBus;
    private readonly OffersPersistence _persistence;
    private readonly ISystemClock _systemClock;

    public PassExpiredEventConsumer(
        IEventBus eventBus,
        OffersPersistence persistence,
        ISystemClock systemClock)
    {
        _eventBus = eventBus;
        _persistence = persistence;
        _systemClock = systemClock;
    }

    public async Task Consume(ConsumeContext<PassExpiredEvent> context)
    {
        var @event = context.Message;
        var offer = Offer.PrepareStandardPassExtension(@event.CustomerId, _systemClock.Now);
        _persistence.Offers.Add(offer);
        await _persistence.SaveChangesAsync(context.CancellationToken);

        var offerPreparedEvent = OfferPrepareEvent.Create(offer.Id, offer.CustomerId);
        await _eventBus.PublishAsync(offerPreparedEvent, context.CancellationToken);
    }
}
