namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract;

using PrepareContract;

public class SignContractTests
{
    [Theory]
    [MemberData(nameof(SignContractTestData.GetDates), MemberType = typeof(SignContractTestData))]
    internal void Given_sign_contract_Then_expiration_date_is_set_to_contract_duration_from_now(
        DateTimeOffset fakeNow,
        DateTimeOffset signedAt,
        DateTimeOffset preparedAt,
        DateTimeOffset expectedExpirationDate)
    {
        // Arrange
        var contract = PrepareContract(preparedAt);

        // Act
        contract.Sign(signedAt, fakeNow);

        // Assert
        contract.ExpiringAt.Should().Be(expectedExpirationDate);
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
