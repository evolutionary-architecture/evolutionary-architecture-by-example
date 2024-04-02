namespace EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;

using Contracts.IntegrationEvents;
using DataAccess;
using DataAccess.Database;
using MassTransit;

public sealed class ContractSignedEventConsumer(
    PassesPersistence persistence,
    TimeProvider timeProvider) : IConsumer<ContractSignedEvent>
{
    public async Task Consume(ConsumeContext<ContractSignedEvent> context)
    {
        var @event = context.Message;
        var pass = Pass.Register(@event.ContractCustomerId, @event.SignedAt, @event.ExpireAt);
        await persistence.Passes.AddAsync(pass, context.CancellationToken);
        await persistence.SaveChangesAsync(context.CancellationToken);

        var passRegisteredEvent = PassRegisteredEvent.Create(pass.Id, timeProvider.GetUtcNow());
        await context.Publish(passRegisteredEvent, context.CancellationToken);
    }
}
