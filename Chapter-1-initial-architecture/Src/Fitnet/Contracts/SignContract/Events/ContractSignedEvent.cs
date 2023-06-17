namespace EvolutionaryArchitecture.Fitnet.Contracts.SignContract.Events;

using EvolutionaryArchitecture.Fitnet.Shared.Events;

internal record ContractSignedEvent(Guid Id, Guid ContractId, Guid ContractCustomerId, DateTimeOffset ValidityFrom, DateTimeOffset ValidityTo, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static ContractSignedEvent Create(Guid contractId, Guid contractCustomerId, DateTimeOffset validityFrom, DateTimeOffset validityTo) =>
        new(Guid.NewGuid(), contractId, contractCustomerId, validityFrom, validityTo, DateTimeOffset.UtcNow);
}