namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.SignContract;

public sealed record SignContractCommand(Guid Id, string Signature, DateTimeOffset SignedAt) : ICommand<ErrorOr<Guid>>;
