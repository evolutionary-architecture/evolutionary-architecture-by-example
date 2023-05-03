namespace EvolutionaryArchitecture.Fitnet.Offers.Prepare;

using Shared.Events;

internal sealed record OfferPrepareEvent(Guid Id, Guid PassId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    private OfferPrepareEvent(Guid passId) : this(Guid.NewGuid(), passId, DateTimeOffset.Now)
    {
    }
    
    internal static OfferPrepareEvent Create(Guid passId) => new(passId);
}