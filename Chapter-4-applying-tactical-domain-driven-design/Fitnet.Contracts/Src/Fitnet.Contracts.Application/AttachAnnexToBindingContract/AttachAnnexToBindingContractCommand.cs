namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.AttachAnnexToBindingContract;

public sealed record AttachAnnexToBindingContractCommand(Guid BindingContractId, DateTimeOffset ValidFrom)
    : ICommand<ErrorOr<Guid>>;
