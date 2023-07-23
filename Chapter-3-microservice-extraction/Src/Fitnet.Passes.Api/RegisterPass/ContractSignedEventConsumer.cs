namespace EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;

using Common.Infrastructure.Events.EventBus.InMemory;
using Contracts.IntegrationEvents;
using DataAccess;
using DataAccess.Database;
using MassTransit;

internal sealed class ContractSignedEventConsumer : IConsumer<ContractSignedEvent>
{
    private readonly PassesPersistence _persistence;
    private readonly IInMemoryEventBus _eventBus;

    public ContractSignedEventConsumer(
        PassesPersistence persistence,
        IInMemoryEventBus eventBus)
    {
        _persistence = persistence;
        _eventBus = eventBus;
    }
    
    public async Task Consume(ConsumeContext<ContractSignedEvent> context)
    {
        var @event = context.Message;
        var pass = Pass.Register(@event.ContractCustomerId, @event.SignedAt, @event.ExpireAt);
        await _persistence.Passes.AddAsync(pass, context.CancellationToken);
        await _persistence.SaveChangesAsync(context.CancellationToken);

        var passRegisteredEvent = PassRegisteredEvent.Create(pass.Id);
        await _eventBus.PublishAsync(passRegisteredEvent, context.CancellationToken);
    }
}