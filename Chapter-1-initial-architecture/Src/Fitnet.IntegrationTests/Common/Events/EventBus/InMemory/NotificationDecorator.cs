namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Common.Events.EventBus.InMemory;

using Fitnet.Common.Events;
using Fitnet.Common.Events.EventBus;
using MediatR;

internal class NotificationDecorator<TNotification>(IEventBus eventBus, INotificationHandler<TNotification>? innerHandler)
    : INotificationHandler<TNotification>
    where TNotification : INotification
{
    public async Task Handle(TNotification notification, CancellationToken cancellationToken)
    {
        var @event = notification as IIntegrationEvent;
        await eventBus.PublishAsync(@event!, cancellationToken);

        if (innerHandler is not null)
        {
            await innerHandler.Handle(notification, cancellationToken);
        }
    }
}
