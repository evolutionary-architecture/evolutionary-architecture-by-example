namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Sign;

using Common.Api.ErrorHandling;
using Common.Core.SystemClock;
using Core;
using IntegrationEvents;
using MassTransit;

[UsedImplicitly]
internal sealed class SignContractCommandHandler(
    IContractsRepository contractsRepository,
    IBindingContractsRepository bindingContractsRepository,
    ISystemClock systemClock,
    IPublishEndpoint publishEndpoint) : IRequestHandler<SignContractCommand>
{
    public async Task Handle(SignContractCommand command, CancellationToken cancellationToken)
    {
        var contract = await contractsRepository.GetByIdAsync(command.Id, cancellationToken) ??
                       throw new ResourceNotFoundException(command.Id);
        var bindingContract = contract.Sign(command.SignedAt, systemClock.Now);
        await bindingContractsRepository.AddAsync(bindingContract, cancellationToken);
        await contractsRepository.CommitAsync(cancellationToken);
        var @event = ContractSignedEvent.Create(contract.Id,
                                                        contract.CustomerId,
                                                        contract.SignedAt!.Value,
                                                        contract.ExpiringAt!.Value);
        await publishEndpoint.Publish(@event, cancellationToken);
    }
}
