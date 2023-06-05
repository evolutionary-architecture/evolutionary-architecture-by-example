namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Commands.Preapre;

using EvolutionaryArchitecture.Fitnet.Contracts.Core;

internal sealed class PrepareContractCommandHandler : IRequestHandler<PrepareContractCommand, Guid>
{
    private readonly IContractsRepository _contractsRepository;
    
    public PrepareContractCommandHandler(IContractsRepository contractsRepository) => 
        _contractsRepository = contractsRepository;
    
    public async Task<Guid> Handle(PrepareContractCommand command, CancellationToken cancellationToken)
    {
        var previousContract = await _contractsRepository.GetPreviousForCustomerAsync(command.CustomerId, cancellationToken);
        var contract = Contract.Prepare(command.CustomerId, command.CustomerAge, command.CustomerHeight, command.PreparedAt, previousContract?.Signed);
        await _contractsRepository.AddAsync(contract, cancellationToken);

        return contract.Id;
    }

}