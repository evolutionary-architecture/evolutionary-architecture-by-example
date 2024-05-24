namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.AttachAnnexToBindingContract;

[UsedImplicitly]
internal sealed class AttachAnnexToBindingContractCommandHandler(
    IBindingContractsRepository bindingContractsRepository,
    TimeProvider timeProvider) : IRequestHandler<AttachAnnexToBindingContractCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(AttachAnnexToBindingContractCommand command,
        CancellationToken cancellationToken) =>
        await bindingContractsRepository.GetByIdAsync(command.BindingContractId, cancellationToken)
            .ThenAsync(bindingContract => bindingContract.AttachAnnex(command.ValidFrom, timeProvider.GetUtcNow())
                .ThenAsync(async annexId =>
                {
                    await bindingContractsRepository.CommitAsync(cancellationToken);

                    return annexId.Value;
                }));
}
