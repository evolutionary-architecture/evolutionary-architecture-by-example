namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events;

using MediatR;

public interface IIntegrationEvent : INotification
{
    Guid Id { get; }
    DateTimeOffset OccurredDateTime { get; }
}