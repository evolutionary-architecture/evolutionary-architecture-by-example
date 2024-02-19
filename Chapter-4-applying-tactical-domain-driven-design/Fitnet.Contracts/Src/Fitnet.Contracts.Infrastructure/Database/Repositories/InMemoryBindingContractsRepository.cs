namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Repositories;

using Core;

internal sealed class InMemoryBindingContractsRepository : IBindingContractsRepository
{
    private readonly Dictionary<Guid, BindingContract> _contracts = [];

    public Task<BindingContract?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _contracts.TryGetValue(id, out var contract)
            ? Task.FromResult<BindingContract?>(contract)
            : Task.FromResult<BindingContract?>(null);
    }

    public Task AddAsync(BindingContract contract, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _contracts[contract.Id] = contract;
        return Task.CompletedTask;
    }

    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.CompletedTask;
    }
}
