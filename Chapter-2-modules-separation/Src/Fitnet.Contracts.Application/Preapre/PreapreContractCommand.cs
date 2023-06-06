namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Preapre;

public sealed record PrepareContractCommand(Guid CustomerId, int CustomerAge, int CustomerHeight, DateTimeOffset PreparedAt) : ICommand<Guid>;