namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus;

using MassTransit;

internal sealed class EventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;
    
    public EventBus(IPublishEndpoint publishEndpoint) => _publishEndpoint = publishEndpoint;

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IIntegrationEvent => 
        await _publishEndpoint.Publish(@event, cancellationToken);
}