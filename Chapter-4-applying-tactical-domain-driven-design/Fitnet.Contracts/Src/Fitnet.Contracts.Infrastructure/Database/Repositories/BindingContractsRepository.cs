namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Repositories;

using Application;
using Core;
using ErrorOr;

internal sealed class BindingContractsRepository(ContractsPersistence persistence) : IBindingContractsRepository
{
    public async Task<ErrorOr<BindingContract>> GetByIdAsync(Guid bindingContractId,
        CancellationToken cancellationToken = default)
    {
        var bindingContract = await persistence.BindingContracts.FindAsync([new BindingContractId(bindingContractId)],
            cancellationToken);

        return bindingContract is null
            ? Error.NotFound(nameof(BindingContract), $"Binding contract with id {bindingContractId} not found")
            : bindingContract;
    }

    public async Task AddAsync(BindingContract bindingContract, CancellationToken cancellationToken = default)
    {
        await persistence.BindingContracts.AddAsync(bindingContract, cancellationToken);
        await persistence.SaveChangesAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default) =>
        await persistence.SaveChangesAsync(cancellationToken);
}
