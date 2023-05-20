namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Prepare;

public sealed record PrepareContractRequest(int CustomerAge, int CustomerHeight, DateTimeOffset PreparedAt);