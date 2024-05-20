namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract;

using Common;
using Common.Builders;
using Core.SignContract;

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
            .Prepared()
            .PreparedAt(preparedAt);

        // Act
        var signResult = contract.Sign(signedAt, fakeNow);

        // Assert
        var @event = signResult.Value.GetPublishedEvent<BindingContractStartedEvent>();
        @event?.ExpiringAt.Should().Be(expectedExpirationDate);
    }

    private static readonly DateTimeOffset FakeNow = FakeContractDates.PreparedAt.AddDays(1);
    private static readonly DateTimeOffset SignedAt = FakeContractDates.PreparedAt.AddDays(1);

    [Fact]
    internal void Given_sign_contract_Then_contracts_becomes_binding_contract()
    {
        // Arrange
        Contract contract = ContractBuilder
            .Prepared();

        // Act
        var signResult = contract.Sign(SignedAt, FakeNow);

        // Assert
        var @event = signResult.Value.GetPublishedEvent<BindingContractStartedEvent>();
        @event?.BindingFrom.Should().Be(SignedAt);
    }
}
