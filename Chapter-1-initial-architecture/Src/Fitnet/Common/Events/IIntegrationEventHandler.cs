namespace EvolutionaryArchitecture.Fitnet.Common.Events;

using MediatR;

internal interface IIntegrationEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IIntegrationEvent;
