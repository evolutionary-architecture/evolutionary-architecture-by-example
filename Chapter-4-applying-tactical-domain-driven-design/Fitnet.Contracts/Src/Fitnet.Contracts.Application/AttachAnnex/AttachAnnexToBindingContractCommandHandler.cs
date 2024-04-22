namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.AttachAnnex;

using Common.Api.ErrorHandling;

[UsedImplicitly]
internal sealed class AttachAnnexToBindingContractCommandHandler(
    IBindingContractsRepository bindingContractsRepository) : IRequestHandler<AttachAnnexToBindingContractCommand>
{
    public async Task Handle(AttachAnnexToBindingContractCommand command, CancellationToken cancellationToken)
    {
        var contract =
            await bindingContractsRepository.GetByContractIdAsync(command.BindingContractId, cancellationToken) ??
            throw new ResourceNotFoundException(command.BindingContractId);
        contract.AttachAnnex(command.ValidFrom);
        await bindingContractsRepository.CommitAsync(cancellationToken);
    }
}
