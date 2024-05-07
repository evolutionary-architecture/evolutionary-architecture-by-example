namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.PrepareContract;

using Core;

[UsedImplicitly]
internal sealed class PrepareContractCommandHandler(IContractsRepository contractsRepository)
    : IRequestHandler<PrepareContractCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(PrepareContractCommand command, CancellationToken cancellationToken)
    {
        var previousContract =
            await contractsRepository.GetPreviousForCustomerAsync(command.CustomerId, cancellationToken);
        return await Contract.Prepare(
                command.CustomerId,
                command.CustomerAge,
                command.CustomerHeight,
                command.PreparedAt,
                previousContract?.IsSigned)
            .ThenAsync(async contract =>
            {
                await contractsRepository.AddAsync(contract, cancellationToken);
                await contractsRepository.CommitAsync(cancellationToken);

                return contract.Id.Value;
            });
    }
}
