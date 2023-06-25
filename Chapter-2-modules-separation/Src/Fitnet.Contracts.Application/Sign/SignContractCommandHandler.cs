namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Sign;

using Common.Api.ErrorHandling;
using Common.Infrastructure.Events.EventBus;
using Common.Infrastructure.SystemClock;
using Core;

[UsedImplicitly]
internal sealed class SignContractCommandHandler : IRequestHandler<SignContractCommand>
{
    private readonly IContractsRepository _contractsRepository;
    private readonly ISystemClock _systemClock;
    private readonly IEventBus _eventBus;

    public SignContractCommandHandler(IContractsRepository contractsRepository, ISystemClock systemClock, IEventBus eventBus)
    {
        _contractsRepository = contractsRepository;
        _systemClock = systemClock;
        _eventBus = eventBus;
    }

    public async Task Handle(SignContractCommand command, CancellationToken cancellationToken)
    {
        var contract = await _contractsRepository.GetByIdAsync(command.Id, cancellationToken);
        if (contract is null)
           throw new ResourceNotFoundException(command.Id);
        
        contract.Sign(command.SignedAt, _systemClock);
        await _contractsRepository.CommitAsync(cancellationToken);
        var @event = ContractSignedEvent.Create(contract.Id, contract.CustomerId, contract.SignedAt!.Value, contract.ExpiringAt!.Value);
        await _eventBus.PublishAsync(@event, cancellationToken);
    }
}