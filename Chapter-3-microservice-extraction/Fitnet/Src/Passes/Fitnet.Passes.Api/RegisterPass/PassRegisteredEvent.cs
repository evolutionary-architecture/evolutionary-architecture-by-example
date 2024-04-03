namespace EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;

using Fitnet.Common.Infrastructure.Events;

internal record PassRegisteredEvent(Guid Id, Guid PassId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static PassRegisteredEvent Create(Guid passId, DateTimeOffset occurredAt) =>
        new(Guid.NewGuid(), passId, occurredAt);
}
