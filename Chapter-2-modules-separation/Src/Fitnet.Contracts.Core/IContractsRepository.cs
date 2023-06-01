namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

public interface IContractsRepository
{
    Task<Contract?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Contract?> GetNotSignedForCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task AddAsync(Contract contract, CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
}