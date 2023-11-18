namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus.InMemory;

using MediatR;

internal sealed class InMemoryEventBus(IPublisher mediator) : IEventBus
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IIntegrationEvent =>
        await mediator.Publish(@event, cancellationToken);
}
