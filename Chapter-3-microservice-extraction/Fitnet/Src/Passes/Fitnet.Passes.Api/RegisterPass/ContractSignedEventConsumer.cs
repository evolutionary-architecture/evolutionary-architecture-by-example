namespace EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;

using Contracts.IntegrationEvents;
using DataAccess;
using DataAccess.Database;
using MassTransit;

public sealed class ContractSignedEventConsumer : IConsumer<ContractSignedEvent>
{
    private readonly PassesPersistence _persistence;

    public ContractSignedEventConsumer(
        PassesPersistence persistence) =>
        _persistence = persistence;

    public async Task Consume(ConsumeContext<ContractSignedEvent> context)
    {
        var @event = context.Message;
        var pass = Pass.Register(@event.ContractCustomerId, @event.SignedAt, @event.ExpireAt);
        await _persistence.Passes.AddAsync(pass, context.CancellationToken);
        await _persistence.SaveChangesAsync(context.CancellationToken);

        var passRegisteredEvent = PassRegisteredEvent.Create(pass.Id);
        await context.Publish(passRegisteredEvent, context.CancellationToken);
    }
}
