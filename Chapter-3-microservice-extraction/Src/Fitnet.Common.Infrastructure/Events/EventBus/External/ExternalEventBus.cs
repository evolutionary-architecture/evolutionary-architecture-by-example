namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus.External;

using MassTransit;

internal sealed class ExternalEventBus : IExternalEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;
    
    public ExternalEventBus(IPublishEndpoint publishEndpoint) => _publishEndpoint = publishEndpoint;

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IIntegrationEvent => 
        await _publishEndpoint.Publish(@event, cancellationToken);
}