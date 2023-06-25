namespace EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;

using Common.Infrastructure.Events;
using Common.Infrastructure.Events.EventBus;
using Contracts.Application.Sign;
using DataAccess;
using DataAccess.Database;

internal sealed class ContractSignedEventHandler : IIntegrationEventHandler<ContractSignedEvent>
{
    private readonly PassesPersistence _persistence;
    private readonly IEventBus _eventBus;

    public ContractSignedEventHandler(
        PassesPersistence persistence,
        IEventBus eventBus)
    {
        _persistence = persistence;
        _eventBus = eventBus;
    }

    public async Task Handle(ContractSignedEvent @event, CancellationToken cancellationToken)
    {
        var pass = Pass.Register(@event.ContractCustomerId, @event.SignedAt, @event.ExpireAt);
        await _persistence.Passes.AddAsync(pass, cancellationToken);
        await _persistence.SaveChangesAsync(cancellationToken);

        var passRegisteredEvent = PassRegisteredEvent.Create(pass.Id);
        await _eventBus.PublishAsync(passRegisteredEvent, cancellationToken);
    }
}