namespace EvolutionaryArchitecture.Fitnet.Contracts.SignContract;

using Common.Events.EventBus;
using Data;
using Data.Database;
using Events;
using Microsoft.EntityFrameworkCore;
using PrepareContract;

internal sealed class ContractService(ContractsPersistence persistence, IEventBus bus, TimeProvider timeProvider)
{
    public async Task<Contract?> FindContractAsync(Guid id) => await persistence.Contracts.FindAsync(id);

    public async Task SignContractAsync(Contract contract, DateTimeOffset signedAt, CancellationToken cancellationToken)
    {
        if ((signedAt.Date - contract.PreparedAt.Date) > TimeSpan.FromDays(30))
        {
            throw new ArgumentException("Contract can not be signed because more than 30 days have passed from the contract preparation");
        }

        var dateNow = timeProvider.GetUtcNow();
        contract.SignedAt = signedAt;
        contract.ExpiringAt = dateNow.Add(contract.Duration);
        await persistence.SaveChangesAsync(cancellationToken);

        var @event = ContractSignedEvent.Create(
            contract.Id,
            contract.CustomerId,
            contract.SignedAt!.Value,
            contract.ExpiringAt!.Value,
            timeProvider.GetUtcNow());
        await bus.PublishAsync(@event, cancellationToken);
    }

    public async Task PrepareContractAsync(PrepareContractRequest request)
    {
        var previousContract = await GetPreviousForCustomerAsync(persistence, request.CustomerId);

        if (request.CustomerAge < 18)
        {
            if (request.CustomerHeight > 210)
            {
                if (previousContract?.Signed is false)
                {
                    throw new ArgumentException("Previous contract must be signed by the customer");
                }

                throw new ArgumentException("Customer height must fit maximum limit for gym instruments");
            }

            throw new ArgumentException("Contract can not be prepared for a person who is not adult");
        }

        var contract = Contract.Create(request.CustomerId, request.PreparedAt);
        await persistence.Contracts.AddAsync(contract);
        await persistence.SaveChangesAsync();
    }

    private static async Task<Contract?> GetPreviousForCustomerAsync(ContractsPersistence persistence, Guid customerId, CancellationToken cancellationToken = default) =>
        await persistence.Contracts
            .OrderByDescending(contract => contract.PreparedAt)
            .SingleOrDefaultAsync(contract => contract.CustomerId == customerId, cancellationToken);
}
