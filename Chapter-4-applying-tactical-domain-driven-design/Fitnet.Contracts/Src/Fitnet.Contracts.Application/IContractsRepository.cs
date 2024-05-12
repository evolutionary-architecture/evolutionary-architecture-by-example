namespace EvolutionaryArchitecture.Fitnet.Contracts.Application;

using Core;

public interface IContractsRepository
{
    Task<ErrorOr<Contract>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Contract?> GetPreviousForCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task AddAsync(Contract contract, CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
}
