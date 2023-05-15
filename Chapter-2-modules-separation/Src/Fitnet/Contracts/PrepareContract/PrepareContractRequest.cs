namespace EvolutionaryArchitecture.Fitnet.Contracts.PrepareContract;

public sealed record PrepareContractRequest(int CustomerAge, int CustomerHeight, DateTimeOffset PreparedAt);