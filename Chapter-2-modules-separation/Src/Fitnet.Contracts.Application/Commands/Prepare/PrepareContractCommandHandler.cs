namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Commands.Prepare;

using Core;
using Exceptions;

internal sealed class PrepareContractCommandHandler : IRequestHandler<PrepareContractCommand, Guid>
{
    private readonly IContractsRepository _contractsRepository;
    
    public PrepareContractCommandHandler(IContractsRepository contractsRepository) => 
        _contractsRepository = contractsRepository;
    
    public async Task<Guid> Handle(PrepareContractCommand command, CancellationToken cancellationToken)
    {
        var existingContract = await _contractsRepository.GetNotSignedForCustomerAsync(command.CustomerId, cancellationToken);
        if (existingContract is not null)
        {
            throw new CustomerHasNotSignedContractException(existingContract.CustomerId, existingContract.Id, existingContract.PreparedAt);
        }
        
        var contract = Contract.Prepare(command.CustomerId, command.CustomerAge, command.CustomerHeight, command.PreparedAt);
        await _contractsRepository.AddAsync(contract, cancellationToken);

        return contract.Id;
    }
}