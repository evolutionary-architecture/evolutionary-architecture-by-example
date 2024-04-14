namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Sign;

using Common.Api.ErrorHandling;
using IntegrationEvents;
using MassTransit;

[UsedImplicitly]
internal sealed class SignContractCommandHandler(
    IContractsRepository contractsRepository,
    TimeProvider timeProvider,
    IPublishEndpoint publishEndpoint) : IRequestHandler<SignContractCommand>
{
    public async Task Handle(SignContractCommand command, CancellationToken cancellationToken)
    {
        var contract = await contractsRepository.GetByIdAsync(command.Id, cancellationToken) ??
                       throw new ResourceNotFoundException(command.Id);
        contract.Sign(command.SignedAt, timeProvider.GetUtcNow());
        await contractsRepository.CommitAsync(cancellationToken);
        var @event = ContractSignedEvent.Create(contract.Id,
                                                        contract.CustomerId,
                                                        contract.SignedAt!.Value,
                                                        contract.ExpiringAt!.Value,
                                                        timeProvider.GetUtcNow());
        await publishEndpoint.Publish(@event, cancellationToken);
    }
}
