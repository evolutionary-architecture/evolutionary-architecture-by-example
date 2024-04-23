namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.SignContract;

using Common.Api.ErrorHandling;
using ErrorOr;
using IntegrationEvents;
using MassTransit;

[UsedImplicitly]
internal sealed class SignContractCommandHandler(
    IContractsRepository contractsRepository,
    IBindingContractsRepository bindingContractsRepository,
    TimeProvider timeProvider,
    IPublishEndpoint publishEndpoint) : IRequestHandler<SignContractCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(SignContractCommand command, CancellationToken cancellationToken)
    {
        var contract = await contractsRepository.GetByIdAsync(command.Id, cancellationToken) ??
                       throw new ResourceNotFoundException(command.Id);
        var now = timeProvider.GetUtcNow();
        var signResult = contract.Sign(command.SignedAt, now);
        if (signResult.IsError)
        {
            return signResult.Errors;
        }

        await bindingContractsRepository.AddAsync(signResult.Value, cancellationToken);
        await contractsRepository.CommitAsync(cancellationToken);
        var @event = ContractSignedEvent.Create(contract.Id.Value,
                                                        contract.CustomerId,
                                                        contract.SignedAt!.Value,
                                                        contract.ExpiringAt!.Value);
        await publishEndpoint.Publish(@event, cancellationToken);

        return Unit.Value;
    }
}
