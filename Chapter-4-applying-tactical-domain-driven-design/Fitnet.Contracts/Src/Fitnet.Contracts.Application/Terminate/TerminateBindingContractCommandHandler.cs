namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Terminate;

using Common.Api.ErrorHandling;
using EvolutionaryArchitecture.Fitnet.Common.Core.SystemClock;
using Core;

[UsedImplicitly]
internal sealed class TerminateBindingContractCommandHandler(
    IBindingContractsRepository bindingContractsRepository,
    ISystemClock systemClock) : IRequestHandler<TerminateBindingContractCommand>
{
    public async Task Handle(TerminateBindingContractCommand command, CancellationToken cancellationToken)
    {
        var contract = await bindingContractsRepository.GetByIdAsync(command.Id, cancellationToken) ??
                       throw new ResourceNotFoundException(command.Id);
        contract.Terminate(systemClock.Now);
        await bindingContractsRepository.CommitAsync(cancellationToken);
    }
}
