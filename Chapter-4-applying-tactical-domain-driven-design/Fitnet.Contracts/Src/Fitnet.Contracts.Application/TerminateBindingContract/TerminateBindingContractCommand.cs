namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.TerminateBindingContract;

public sealed record TerminateBindingContractCommand(Guid BindingContractId) : ICommand;
