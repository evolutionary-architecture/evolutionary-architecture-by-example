namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.AttachAnnexToBindingContract;

using Common.Api.ErrorHandling;

[UsedImplicitly]
internal sealed class AttachAnnexToBindingContractCommandHandler(
    IBindingContractsRepository bindingContractsRepository,
    TimeProvider timeProvider) : IRequestHandler<AttachAnnexToBindingContractCommand>
{
    public async Task Handle(AttachAnnexToBindingContractCommand command, CancellationToken cancellationToken)
    {
        var bindingContract =
            await bindingContractsRepository.GetByIdAsync(command.BindingContractId, cancellationToken) ??
            throw new ResourceNotFoundException(command.BindingContractId);
        bindingContract.AttachAnnex(command.ValidFrom, timeProvider.GetUtcNow());
        await bindingContractsRepository.CommitAsync(cancellationToken);
    }
}
