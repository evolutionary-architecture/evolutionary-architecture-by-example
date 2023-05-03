namespace EvolutionaryArchitecture.Fitnet.Shared.Events;

using MediatR;

internal interface IIntegrationEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IIntegrationEvent
{
}
