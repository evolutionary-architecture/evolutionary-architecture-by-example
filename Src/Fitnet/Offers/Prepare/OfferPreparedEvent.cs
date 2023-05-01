namespace SuperSimpleArchitecture.Fitnet.Offers.Prepare;

using Shared.Events;

internal sealed record OfferPreparedEvent(Guid Id, Guid PassId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    private OfferPreparedEvent(Guid passId) : this(Guid.NewGuid(), passId, DateTimeOffset.Now)
    {
    }
    
    internal static OfferPreparedEvent Create(Guid passId) => new(passId);
}