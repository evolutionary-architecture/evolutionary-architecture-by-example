namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Repositories;

using Core;

internal sealed class ContractsRepository : IContractsRepository
{
    private readonly ContractsPersistence _persistence;
    
    public ContractsRepository(ContractsPersistence persistence) => 
        _persistence = persistence;
   
    public async Task<Contract?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => 
        await _persistence.Contracts.FindAsync(new object?[] { id }, cancellationToken);
   
    public async Task AddAsync(Contract contract, CancellationToken cancellationToken = default) => 
        await _persistence.Contracts.AddAsync(contract, cancellationToken);

    public async Task CommitAsync(CancellationToken cancellationToken = default) => 
        await _persistence.SaveChangesAsync(cancellationToken);
}