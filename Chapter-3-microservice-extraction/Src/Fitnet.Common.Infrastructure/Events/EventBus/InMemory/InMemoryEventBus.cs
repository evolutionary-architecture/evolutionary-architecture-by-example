namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus.InMemory;

using MediatR;

internal sealed class InMemoryEventBus : IInMemoryEventBus
{
    private readonly IMediator _mediator;
    
    public InMemoryEventBus(IMediator mediator) => _mediator = mediator;

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IIntegrationEvent => 
        await _mediator.Publish(@event, cancellationToken);
}