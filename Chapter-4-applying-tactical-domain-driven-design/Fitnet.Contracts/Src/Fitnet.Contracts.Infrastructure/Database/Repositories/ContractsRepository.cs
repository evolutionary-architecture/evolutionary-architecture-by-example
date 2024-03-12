namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Repositories;

using Core;
using Microsoft.EntityFrameworkCore;

internal sealed class ContractsRepository(ContractsPersistence persistence) : IContractsRepository
{
    public async Task<Contract?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await persistence.Contracts.FindAsync([new ContractId(id)], cancellationToken);

    public async Task<Contract?> GetPreviousForCustomerAsync(Guid customerId, CancellationToken cancellationToken = default) =>
        await persistence.Contracts
            .OrderByDescending(contract => contract.PreparedAt)
            .SingleOrDefaultAsync(contract => contract.CustomerId == customerId, cancellationToken);

    public async Task AddAsync(Contract contract, CancellationToken cancellationToken = default)
    {
        await persistence.Contracts.AddAsync(contract, cancellationToken);
        await persistence.SaveChangesAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default) =>
        await persistence.SaveChangesAsync(cancellationToken);
}
