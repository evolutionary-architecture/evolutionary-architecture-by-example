namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.AttachAnnex;

public sealed record AttachAnnexToBindingContractCommand(Guid BindingContractId, DateTimeOffset ValidFrom) : ICommand;
