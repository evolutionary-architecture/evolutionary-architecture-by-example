namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Commands.Prepare;

using Core;
using MediatR;

internal sealed class PrepareContractCommandHandler : IRequestHandler<PrepareContractCommand, Guid>
{
    private readonly IContractsRepository _contractsRepository;
    
    public PrepareContractCommandHandler(IContractsRepository contractsRepository) => 
        _contractsRepository = contractsRepository;
    
    public async Task<Guid> Handle(PrepareContractCommand command, CancellationToken cancellationToken)
    {
        var contract = Contract.Prepare(command.CustomerAge, command.CustomerHeight, command.PreparedAt);
        await _contractsRepository.AddAsync(contract, cancellationToken);

        return contract.Id;
    }
}