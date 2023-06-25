namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Sign;

using Common.Infrastructure.Events;

public record ContractSignedEvent(Guid Id, Guid ContractId, Guid ContractCustomerId, DateTimeOffset SignedAt, DateTimeOffset ExpireAt, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static ContractSignedEvent Create(Guid contractId, Guid contractCustomerId, DateTimeOffset signedAt, DateTimeOffset expireAt) =>
        new(Guid.NewGuid(), contractId, contractCustomerId, signedAt, expireAt, DateTimeOffset.UtcNow);
}