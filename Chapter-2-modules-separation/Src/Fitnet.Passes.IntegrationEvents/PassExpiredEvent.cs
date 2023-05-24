using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events;

namespace EvolutionaryArchitecture.Fitnet.Passes.IntegrationEvents;

public record PassExpiredEvent
    (Guid Id, Guid PassId, Guid CustomerId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    public static PassExpiredEvent Create(Guid passId, Guid customerId) =>
        new(Guid.NewGuid(), passId, customerId, DateTimeOffset.Now);
}