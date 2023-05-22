namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events;

using MediatR;

public interface IIntegrationEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IIntegrationEvent
{
}
