namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Sign;

using Common.Api.ErrorHandling;
using Common.Infrastructure.Events.EventBus;
using Core;
using IntegrationEvents;

[UsedImplicitly]
internal sealed class SignContractCommandHandler(
    IContractsRepository contractsRepository,
    TimeProvider timeProvider,
    IEventBus eventBus) : IRequestHandler<SignContractCommand>
{
    public async Task Handle(SignContractCommand command, CancellationToken cancellationToken)
    {
        var contract = await contractsRepository.GetByIdAsync(command.Id, cancellationToken) ??
                       throw new ResourceNotFoundException(command.Id);

        var nowDate = timeProvider.GetUtcNow();
        contract.Sign(command.SignedAt, nowDate);
        await contractsRepository.CommitAsync(cancellationToken);
        var @event = ContractSignedEvent.Create(contract.Id,
            contract.CustomerId,
            contract.SignedAt!.Value,
            contract.ExpiringAt!.Value,
            timeProvider.GetUtcNow());
        await eventBus.PublishAsync(@event, cancellationToken);
    }
}
