namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

public interface IBindingContractsRepository
{
    Task<BindingContract?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(BindingContract contract, CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
}
