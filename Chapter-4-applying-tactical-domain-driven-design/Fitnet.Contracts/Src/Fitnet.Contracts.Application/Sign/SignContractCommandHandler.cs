namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Sign;

using Common.Api.ErrorHandling;
using Core;
using IntegrationEvents;
using MassTransit;

[UsedImplicitly]
internal sealed class SignContractCommandHandler(
    IContractsRepository contractsRepository,
    IBindingContractsRepository bindingContractsRepository,
    TimeProvider timeProvider,
    IPublishEndpoint publishEndpoint) : IRequestHandler<SignContractCommand>
{
    public async Task Handle(SignContractCommand command, CancellationToken cancellationToken)
    {
        var contract = await contractsRepository.GetByIdAsync(command.Id, cancellationToken) ??
                       throw new ResourceNotFoundException(command.Id);
        var today = timeProvider.GetUtcNow();
        var bindingContract = contract.Sign(command.SignedAt, today);
        await bindingContractsRepository.AddAsync(bindingContract, cancellationToken);
        await contractsRepository.CommitAsync(cancellationToken);
        var @event = ContractSignedEvent.Create(contract.Id.Value,
                                                        contract.CustomerId,
                                                        contract.SignedAt!.Value,
                                                        contract.ExpiringAt!.Value);
        await publishEndpoint.Publish(@event, cancellationToken);
    }
}
