namespace EvolutionaryArchitecture.Fitnet.Offers.Prepare;

using Common.Events;

internal sealed record OfferPrepareEvent(Guid Id, Guid OfferId, Guid CustomerId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static OfferPrepareEvent Create(Guid offerId, Guid customerId, DateTimeOffset occurredAt) =>
        new(Guid.NewGuid(), offerId, customerId, occurredAt);
}
