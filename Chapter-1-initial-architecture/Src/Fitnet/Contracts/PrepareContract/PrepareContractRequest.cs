namespace EvolutionaryArchitecture.Fitnet.Contracts.PrepareContract;

public sealed record PrepareContractRequest(Guid CustomerId, int CustomerAge, int CustomerHeight, DateTimeOffset PreparedAt);