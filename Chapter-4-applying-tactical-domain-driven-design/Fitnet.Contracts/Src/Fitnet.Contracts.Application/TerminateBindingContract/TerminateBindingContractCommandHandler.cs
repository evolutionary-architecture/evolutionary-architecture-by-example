namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.TerminateBindingContract;

using Common.Api.ErrorHandling;
using Core;

[UsedImplicitly]
internal sealed class TerminateBindingContractCommandHandler(
    IBindingContractsRepository bindingContractsRepository,
    TimeProvider timeProvider) : IRequestHandler<TerminateBindingContractCommand>
{
    public async Task Handle(TerminateBindingContractCommand command, CancellationToken cancellationToken)
    {
        var contract = await bindingContractsRepository.GetByContractIdAsync(command.Id, cancellationToken) ??
                       throw new ResourceNotFoundException(command.Id);
        var terminatedAt = timeProvider.GetUtcNow();
        contract.Terminate(terminatedAt);
        await bindingContractsRepository.CommitAsync(cancellationToken);
    }
}
