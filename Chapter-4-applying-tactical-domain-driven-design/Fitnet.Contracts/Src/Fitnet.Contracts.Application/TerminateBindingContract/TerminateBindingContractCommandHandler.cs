namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.TerminateBindingContract;

using Common.Api.ErrorHandling;
using ErrorOr;

[UsedImplicitly]
internal sealed class TerminateBindingContractCommandHandler(
    IBindingContractsRepository bindingContractsRepository,
    TimeProvider timeProvider) : IRequestHandler<TerminateBindingContractCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(TerminateBindingContractCommand command, CancellationToken cancellationToken)
    {
        var contract = await bindingContractsRepository.GetByIdAsync(command.BindingContractId, cancellationToken) ??
                       throw new ResourceNotFoundException(command.BindingContractId);
        var terminatedAt = timeProvider.GetUtcNow();
        contract.Terminate(terminatedAt);
        await bindingContractsRepository.CommitAsync(cancellationToken);

        return Unit.Value;
    }
}
