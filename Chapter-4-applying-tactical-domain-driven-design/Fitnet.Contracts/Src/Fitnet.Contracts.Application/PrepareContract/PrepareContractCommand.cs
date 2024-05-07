namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.PrepareContract;

public sealed record PrepareContractCommand(Guid CustomerId, int CustomerAge, int CustomerHeight, DateTimeOffset PreparedAt) : ICommand<ErrorOr<Guid>>;
