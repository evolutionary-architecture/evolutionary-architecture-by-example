namespace EvolutionaryArchitecture.Fitnet.Contracts.SignContract.Events;

using EvolutionaryArchitecture.Fitnet.Shared.Events;

internal record ContractSignedEvent(Guid Id, Guid ContractId, Guid ContractCustomerId, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static ContractSignedEvent Create(Guid contractId, Guid contractCustomerId) =>
        new(Guid.NewGuid(), contractId, contractCustomerId, DateTimeOffset.UtcNow);
}