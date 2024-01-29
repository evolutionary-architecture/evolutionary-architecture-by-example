namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Prepare;

using Core;

[UsedImplicitly]
internal sealed class PrepareContractCommandHandler(IContractsRepository contractsRepository) : IRequestHandler<PrepareContractCommand, Guid>
{
    public async Task<Guid> Handle(PrepareContractCommand command, CancellationToken cancellationToken)
    {
        var previousContract = await contractsRepository.GetPreviousForCustomerAsync(command.CustomerId, cancellationToken);
        var contract = Contract.Prepare(command.CustomerId, command.CustomerAge, command.CustomerHeight, command.PreparedAt, previousContract?.IsSigned);
        await contractsRepository.AddAsync(contract, cancellationToken);

        return contract.Id;
    }
}
