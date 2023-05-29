namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Commands.Sign;

public sealed record SignContractCommand(Guid Id, DateTimeOffset SignedAt) : ICommand;