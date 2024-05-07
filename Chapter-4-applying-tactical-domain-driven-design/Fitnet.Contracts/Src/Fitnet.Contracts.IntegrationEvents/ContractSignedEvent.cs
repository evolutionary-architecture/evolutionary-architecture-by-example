namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationEvents;

public sealed record ContractSignedEvent(
    Guid Id,
    Guid ContractId,
    Guid ContractCustomerId,
    DateTimeOffset SignedAt,
    DateTimeOffset ExpireAt,
    DateTimeOffset OccurredDateTime)
{
    public static ContractSignedEvent Create(Guid contractId, Guid contractCustomerId, DateTimeOffset signedAt,
        DateTimeOffset expireAt) =>
        new(Guid.NewGuid(), contractId, contractCustomerId, signedAt, expireAt, DateTimeOffset.UtcNow);
}
