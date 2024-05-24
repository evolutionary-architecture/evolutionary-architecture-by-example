namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.TerminateBindingContract;

using ErrorOr;

[UsedImplicitly]
internal sealed class TerminateBindingContractCommandHandler(
    IBindingContractsRepository bindingContractsRepository,
    TimeProvider timeProvider) : IRequestHandler<TerminateBindingContractCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(TerminateBindingContractCommand command,
        CancellationToken cancellationToken) =>
        await bindingContractsRepository.GetByIdAsync(command.BindingContractId, cancellationToken)
            .ThenAsync(bindingContract => bindingContract.Terminate(timeProvider.GetUtcNow())
                .ThenAsync(async _ =>
                {
                    await bindingContractsRepository.CommitAsync(cancellationToken);
                    return Unit.Value;
                }));
}
