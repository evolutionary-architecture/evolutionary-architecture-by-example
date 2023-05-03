namespace EvolutionaryArchitecture.Fitnet.Shared.Events.EventBus;

internal interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent;
}