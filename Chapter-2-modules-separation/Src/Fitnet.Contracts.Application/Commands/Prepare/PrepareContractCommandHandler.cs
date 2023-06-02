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
        await EnsureThatCustomerHasNoUnsignedContract(command.CustomerId, cancellationToken);

        var contract = Contract.Prepare(command.CustomerId, command.CustomerAge, command.CustomerHeight, command.PreparedAt);
        await _contractsRepository.AddAsync(contract, cancellationToken);

        return contract.Id;
    }
    
    private async Task EnsureThatCustomerHasNoUnsignedContract(Guid customerId, CancellationToken cancellationToken)
    {
        var unsignedContract = await _contractsRepository.GetNotSignedForCustomerAsync(customerId, cancellationToken);
        var hasCustomerUnsignedContract = unsignedContract is not null;
        if (hasCustomerUnsignedContract)
        {
            throw new CustomerHasUnsignedContractException(unsignedContract!.CustomerId, unsignedContract.Id, unsignedContract.PreparedAt);
        }
    }
}