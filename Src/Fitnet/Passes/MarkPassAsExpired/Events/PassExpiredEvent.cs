namespace SuperSimpleArchitecture.Fitnet.Passes.MarkPassAsExpired.Events;

using SuperSimpleArchitecture.Fitnet.Shared.Events;

internal record PassExpiredEvent(Guid Id, Guid PassId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static PassExpiredEvent Create(Guid passId) => new(Guid.NewGuid(), passId, DateTimeOffset.Now);
}