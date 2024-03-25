namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.TerminateBindingContract;

using Common;
using Core.TerminateBindingContract;
using PrepareContract;

public sealed class TerminateBindingContractTests
{
    [Fact]
    internal void Given_terminate_binding_contracts_Then_should_raise_binding_contracts()
    {
        var preparedAt = new DateTimeOffset(2022, 2, 3, 1, 1, 1, TimeSpan.Zero);
        var terminatedAt = new DateTimeOffset(2023, 3, 3, 1, 1, 1, TimeSpan.Zero);
        var fakeToday = new DateTimeOffset(2022, 3, 4, 1, 1, 1, TimeSpan.Zero);
        var signDay = new DateTimeOffset(2022, 1, 3, 1, 1, 1, TimeSpan.Zero);
        var contract = PrepareContract(preparedAt);
        var bindingContract = contract.Sign(signDay, fakeToday);

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
