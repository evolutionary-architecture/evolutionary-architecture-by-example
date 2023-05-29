namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Commands.Prepare;

public sealed record PrepareContractCommand(int CustomerAge, int CustomerHeight, DateTimeOffset PreparedAt) : ICommand<Guid>;