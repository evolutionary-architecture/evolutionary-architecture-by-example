namespace EvolutionaryArchitecture.Fitnet.Passes.Api.MarkPassAsExpired.Events;

using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events;

internal record PassExpiredEvent(Guid Id, Guid PassId, Guid CustomerId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static PassExpiredEvent Create(Guid passId, Guid customerId) => new(Guid.NewGuid(), passId, customerId, DateTimeOffset.Now);
}