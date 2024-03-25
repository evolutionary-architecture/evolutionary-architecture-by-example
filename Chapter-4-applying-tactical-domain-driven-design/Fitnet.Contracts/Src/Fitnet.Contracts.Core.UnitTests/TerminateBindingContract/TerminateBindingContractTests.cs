namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.TerminateBindingContract;

using Common;
using PrepareContract;
using SignContract;
using TerminateContract;

public sealed class TerminateBindingContractTests
{
    [Theory]
    [ClassData(typeof(SignContractTestData))]
    internal void Given_sign_contract_Then_expiration_date_is_set_to_contract_duration_from_now(
        DateTimeOffset fakeNow,
        DateTimeOffset signedAt,
        DateTimeOffset preparedAt)
    {
        var contract = PrepareContract(preparedAt);
        var bindingContract = contract.Sign(signedAt, fakeNow);
        var terminatedAt = DateTimeOffset.Now;

        bindingContract.Terminate(terminatedAt);

        var @event = bindingContract.GetPublishedEvent<BindingContractTerminatedEvent>();
        @event?.TerminatedAt.Should().Be(terminatedAt);
    }

    private static Contract PrepareContract(DateTimeOffset preparedAt)
    {
        var prepareContractParameters = PrepareContractParameters.GetValid();
        var contract = Contract.Prepare(
            Guid.NewGuid(),
            prepareContractParameters.MaxAge,
            prepareContractParameters.MaxHeight,
            preparedAt);

        return contract;
    }
}
