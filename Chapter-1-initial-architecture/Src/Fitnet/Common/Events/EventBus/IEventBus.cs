namespace EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;

internal interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent;
}