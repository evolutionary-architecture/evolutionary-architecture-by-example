namespace EvolutionaryArchitecture.Fitnet.Offers.Prepare;

using Common.Infrastructure.Events;

internal sealed record OfferPrepareEvent(Guid Id, Guid OfferId, Guid CustomerId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    private OfferPrepareEvent(Guid offerId, Guid customerId) : this(Guid.NewGuid(), offerId, customerId, DateTimeOffset.Now)
    {
    }

    internal static OfferPrepareEvent Create(Guid offerId, Guid customerId) => new(offerId, customerId);
}