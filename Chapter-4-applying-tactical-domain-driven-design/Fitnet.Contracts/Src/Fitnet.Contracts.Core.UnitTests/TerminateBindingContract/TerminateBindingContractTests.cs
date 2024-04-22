namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.TerminateBindingContract;

using Common;
using Common.Builders;
using Core.TerminateBindingContract;

public sealed class TerminateBindingContractTests
{
    private readonly DateTimeOffset _terminatedAt = new(2023, 3, 3, 1, 1, 1, TimeSpan.Zero);

    [Fact]
    internal void Given_terminate_binding_contracts_Then_should_raise_binding_contracts()
    {
        BindingContract bindingContract = ContractBuilder
            .Prepared()
            .Signed();

        bindingContract.Terminate(_terminatedAt);

        var @event = bindingContract.GetPublishedEvent<BindingContractTerminatedEvent>();
        @event?.TerminatedAt.Should().Be(_terminatedAt);
    }
}
