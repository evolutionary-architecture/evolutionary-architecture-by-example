namespace EvolutionaryArchitecture.Fitnet.Contracts.SignContract.Events;

using EvolutionaryArchitecture.Fitnet.Common.Events;

internal record ContractSignedEvent(
    Guid Id,
    Guid ContractId,
    Guid ContractCustomerId,
    DateTimeOffset SignedAt,
    DateTimeOffset ExpireAt,
    DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static ContractSignedEvent Create(
        Guid contractId,
        Guid contractCustomerId,
        DateTimeOffset signedAt,
        DateTimeOffset expireAt,
        DateTimeOffset occurredAt) =>
        new(Guid.NewGuid(), contractId, contractCustomerId, signedAt, expireAt, occurredAt);
}
