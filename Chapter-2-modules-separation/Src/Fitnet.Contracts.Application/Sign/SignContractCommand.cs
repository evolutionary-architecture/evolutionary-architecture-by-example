namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Sign;

public sealed record SignContractCommand(Guid Id, DateTimeOffset SignedAt) : ICommand;