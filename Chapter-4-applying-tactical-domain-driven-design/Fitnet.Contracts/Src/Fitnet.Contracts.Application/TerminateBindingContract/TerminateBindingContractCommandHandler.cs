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
        var contract = await bindingContractsRepository.GetByIdAsync(command.Id, cancellationToken) ??
                       throw new ResourceNotFoundException(command.Id);
        var now = timeProvider.GetUtcNow();
        contract.Terminate(now);
        await bindingContractsRepository.CommitAsync(cancellationToken);
    }
}
