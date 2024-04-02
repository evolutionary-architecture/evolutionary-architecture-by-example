namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.PrepareContract;

using Core;

[UsedImplicitly]
internal sealed class PrepareContractCommandHandler(IContractsRepository contractsRepository, TimeProvider timeProvider)
    : IRequestHandler<PrepareContractCommand, Guid>
{
    public async Task<Guid> Handle(PrepareContractCommand command, CancellationToken cancellationToken)
    {
        var previousContract =
            await contractsRepository.GetPreviousForCustomerAsync(command.CustomerId, cancellationToken);
        var occuredAt = timeProvider.GetUtcNow();
        var contract = Contract.Prepare(
            command.CustomerId,
            command.CustomerAge,
            command.CustomerHeight,
            command.PreparedAt,
            occuredAt,
            previousContract?.IsSigned);
        await contractsRepository.AddAsync(contract, cancellationToken);

        return contract.Id.Value;
    }
}
