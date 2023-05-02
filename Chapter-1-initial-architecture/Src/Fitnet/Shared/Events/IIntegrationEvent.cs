namespace SuperSimpleArchitecture.Fitnet.Shared.Events;

using MediatR;

internal interface IIntegrationEvent : INotification
{
    Guid Id { get; }
    DateTimeOffset OccurredDateTime { get; }
}