namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Commands.Preapre;

public sealed record PrepareContractCommand(Guid CustomerId, int CustomerAge, int CustomerHeight, DateTimeOffset PreparedAt) : ICommand<Guid>;