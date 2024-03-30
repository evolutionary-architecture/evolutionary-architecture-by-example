namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract;

using Common;
using Core.SignContract;
using PrepareContract;

public sealed class SignContractTests
{
    [Theory]
    [ClassData(typeof(SignContractTestData))]
    internal void Given_sign_contract_Then_expiration_date_is_set_to_contract_duration_from_now(
        DateTimeOffset fakeNow,
        DateTimeOffset signedAt,
        DateTimeOffset preparedAt,
        DateTimeOffset expectedExpirationDate)
    {
        // Arrange
        Contract contract = ContractBuilder
            .Create()
            .PreparedAt(preparedAt);

        // Act
        var bindingContract = contract.Sign(signedAt, fakeNow);

        // Assert
        var @event = bindingContract.GetPublishedEvent<ContractStartedBindingEvent>();
        @event?.ExpiringAt.Should().Be(expectedExpirationDate);
    }

    private static readonly DateTimeOffset FakeNow = FakeContractDates.PreparedAt.AddDays(1);
    private static readonly DateTimeOffset SignedAt = FakeContractDates.PreparedAt.AddDays(1);

    [Fact]
    internal void Given_sign_contract_Then_contracts_becomes_binding_contract()
    {
        // Arrange
        Contract contract = ContractBuilder
            .Create();

        // Act
        var bindingContract = contract.Sign(SignedAt, FakeNow);

        // Assert
        var @event = bindingContract.GetPublishedEvent<ContractStartedBindingEvent>();
        @event?.BindingFrom.Should().Be(SignedAt);
    }
}
