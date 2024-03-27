namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.TerminateBindingContract;

using Common;
using Core.TerminateBindingContract;
using SignContract;

public sealed class TerminateBindingContractTests
{
    private readonly DateTimeOffset _preparedAt = new(2022, 2, 3, 1, 1, 1, TimeSpan.Zero);
    private readonly DateTimeOffset _terminatedAt = new(2023, 3, 3, 1, 1, 1, TimeSpan.Zero);
    private readonly DateTimeOffset _fakeToday = new(2022, 3, 4, 1, 1, 1, TimeSpan.Zero);
    private readonly DateTimeOffset _signDay = new(2022, 1, 3, 1, 1, 1, TimeSpan.Zero);

    [Fact]
    internal void Given_terminate_binding_contracts_Then_should_raise_binding_contracts()
    {

        BindingContract bindingContract = ContractBuilder
            .Create()
                .PreparedAt(_preparedAt)
            .Prepared()
                .SignedOn(_signDay, _fakeToday);

        bindingContract.Terminate(_terminatedAt);

        var @event = bindingContract.GetPublishedEvent<BindingContractTerminatedEvent>();
        @event?.TerminatedAt.Should().Be(_terminatedAt);
    }
}
