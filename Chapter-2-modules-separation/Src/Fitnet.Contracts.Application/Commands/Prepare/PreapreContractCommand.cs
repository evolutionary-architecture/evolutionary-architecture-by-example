namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Commands.Prepare;

public sealed record PrepareContractCommand(Guid CustomerId, int CustomerAge, int CustomerHeight, DateTimeOffset PreparedAt) : ICommand<Guid>;