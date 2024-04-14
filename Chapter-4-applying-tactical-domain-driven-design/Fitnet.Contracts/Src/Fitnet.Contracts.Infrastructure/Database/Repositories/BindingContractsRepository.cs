namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Repositories;

using Application;
using Core;
using Microsoft.EntityFrameworkCore;

internal sealed class BindingContractsRepository(ContractsPersistence persistence) : IBindingContractsRepository
{
    public async Task<BindingContract?> GetByContractIdAsync(Guid contractId,
        CancellationToken cancellationToken = default) =>
        await persistence.BindingContracts.Where(bc => bc.ContractId == new ContractId(contractId))
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

    public async Task AddAsync(BindingContract bindingContract, CancellationToken cancellationToken = default)
    {
        await persistence.BindingContracts.AddAsync(bindingContract, cancellationToken);
        await persistence.SaveChangesAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default) =>
        await persistence.SaveChangesAsync(cancellationToken);
}
