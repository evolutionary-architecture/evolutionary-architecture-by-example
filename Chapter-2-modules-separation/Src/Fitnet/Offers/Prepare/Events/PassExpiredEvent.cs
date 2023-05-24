using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events;

namespace EvolutionaryArchitecture.Fitnet.Offers.Prepare.Events;

internal record PassExpiredEvent(Guid Id, Guid CustomerId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static PassExpiredEvent Create(Guid customerId) => new(Guid.NewGuid(), customerId, DateTimeOffset.Now);
}