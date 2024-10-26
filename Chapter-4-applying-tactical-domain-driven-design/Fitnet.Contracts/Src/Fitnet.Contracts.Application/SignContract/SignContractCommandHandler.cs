namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.SignContract;

using Core.SignContract.Signatures;
using IntegrationEvents;
using MassTransit;

[UsedImplicitly]
internal sealed class SignContractCommandHandler(
    IContractsRepository contractsRepository,
    IBindingContractsRepository bindingContractsRepository,
    TimeProvider timeProvider,
    IPublishEndpoint publishEndpoint) : IRequestHandler<SignContractCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(SignContractCommand command, CancellationToken cancellationToken) =>
        await contractsRepository.GetByIdAsync(command.Id, cancellationToken)
            .ThenAsync(async contract => await contract.Sign(Signature.From(command.SignedAt, command.Signature), timeProvider.GetUtcNow())
                .ThenAsync(async bindingContract =>
                {
                    await bindingContractsRepository.AddAsync(bindingContract, cancellationToken);
                    await contractsRepository.CommitAsync(cancellationToken);
                    var @event = ContractSignedEvent.Create(contract.Id.Value,
                        contract.CustomerId,
                        contract.Signature!.Date,
                        contract.ExpiringAt!.Value);
                    await publishEndpoint.Publish(@event, cancellationToken);

                    return bindingContract.Id.Value;
                }));
}
