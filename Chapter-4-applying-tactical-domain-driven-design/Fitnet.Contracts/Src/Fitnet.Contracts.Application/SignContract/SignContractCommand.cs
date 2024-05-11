namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.SignContract;

public sealed record SignContractCommand(Guid Id, DateTimeOffset SignedAt) : ICommand<ErrorOr<Guid>>;
