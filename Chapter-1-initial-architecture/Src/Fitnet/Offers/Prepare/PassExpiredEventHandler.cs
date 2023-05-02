namespace SuperSimpleArchitecture.Fitnet.Offers.Prepare;

using Passes.MarkPassAsExpired.Events;
using Shared.Events;
using Shared.Events.EventBus;

internal sealed class PassExpiredEventHandler: IIntegrationEventHandler<PassExpiredEvent>
{
    private readonly IEventBus _eventBus;
    
    public PassExpiredEventHandler(IEventBus eventBus) => 
        _eventBus = eventBus;

    public async Task Handle(PassExpiredEvent @event, CancellationToken cancellationToken)
    {
        var offerPreparedEvent = OfferPrepareEvent.Create(@event.PassId);
        await _eventBus.PublishAsync(offerPreparedEvent, cancellationToken);
    }
}