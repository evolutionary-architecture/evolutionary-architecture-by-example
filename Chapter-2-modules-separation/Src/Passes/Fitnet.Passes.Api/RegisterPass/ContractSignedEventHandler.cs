namespace EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;

using Common.Infrastructure.Events;
using Common.Infrastructure.Events.EventBus;
using Contracts.IntegrationEvents;
using DataAccess;
using DataAccess.Database;

internal sealed class ContractSignedEventHandler(
    PassesPersistence persistence,
    IEventBus eventBus) : IIntegrationEventHandler<ContractSignedEvent>
{
    public async Task Handle(ContractSignedEvent @event, CancellationToken cancellationToken)
    {
        var pass = Pass.Register(@event.ContractCustomerId, @event.SignedAt, @event.ExpireAt);
        await persistence.Passes.AddAsync(pass, cancellationToken);
        await persistence.SaveChangesAsync(cancellationToken);

        var passRegisteredEvent = PassRegisteredEvent.Create(pass.Id);
        await eventBus.PublishAsync(passRegisteredEvent, cancellationToken);
    }
}
