namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.PrepareContract;

using Common;
using Core.PrepareContract;

public sealed class PrepareContractTests
{
    private readonly Guid _customerId = Guid.NewGuid();
    private const int CustomerAge = 25;
    private const int CustomerHeight = 170;
    private readonly DateTimeOffset _preparedAt = new(2022, 2, 3, 1, 1, 1, TimeSpan.Zero);

    [Fact]
    internal void Given_prepare_contract_Then_should_raise_contract_prepared_event()
    {
        // Act
        var preparationResult = Contract.Prepare(_customerId, CustomerAge, CustomerHeight, _preparedAt);

        // Assert
        var contract = preparationResult.Value;
        var @event = contract.GetPublishedEvent<ContractPreparedEvent>();
        @event?.CustomerId.Should().Be(_customerId);
        @event?.PreparedAt.Should().Be(_preparedAt);
    }
}
