namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.SignContract;

public sealed record SignContractCommand(Guid Id, string SignatureText, DateTimeOffset SignedAt) : ICommand<ErrorOr<Guid>>;
