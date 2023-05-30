namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Commands.Sign;

using Common.Api.ErrorHandling;
using Core;

internal sealed class SignContractCommandHandler : IRequestHandler<SignContractCommand>
{
    private readonly IContractsRepository _contractsRepository;
    
    public SignContractCommandHandler(IContractsRepository contractsRepository) => 
        _contractsRepository = contractsRepository;
    public async Task Handle(SignContractCommand command, CancellationToken cancellationToken)
    {
        var contract = await _contractsRepository.GetByIdAsync(command.Id, cancellationToken);
        if (contract is null)
           throw new ResourceNotFoundException(command.Id);
        
        contract.Sign(command.SignedAt);
        await _contractsRepository.CommitAsync(cancellationToken);
    }
}