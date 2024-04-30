namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Repositories;

using Application;
using Core;

internal sealed class BindingContractsRepository(ContractsPersistence persistence) : IBindingContractsRepository
{
    public async Task<BindingContract?> GetByIdAsync(Guid bindingContractId,
        CancellationToken cancellationToken = default) =>
        await persistence.BindingContracts.FindAsync([new BindingContractId(bindingContractId)], cancellationToken);

    public async Task AddAsync(BindingContract bindingContract, CancellationToken cancellationToken = default)
    {
        await persistence.BindingContracts.AddAsync(bindingContract, cancellationToken);
        await persistence.SaveChangesAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default) =>
        await persistence.SaveChangesAsync(cancellationToken);
}
