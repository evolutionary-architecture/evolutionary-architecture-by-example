namespace EvolutionaryArchitecture.Fitnet.Passes.IntegrationEvents.MarkPassAsExpired;

using Common.Infrastructure.Events;

public record PassExpiredEvent
    (Guid Id, Guid PassId, Guid CustomerId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    public static PassExpiredEvent Create(Guid passId, Guid customerId) =>
        new(Guid.NewGuid(), passId, customerId, DateTimeOffset.Now);
}