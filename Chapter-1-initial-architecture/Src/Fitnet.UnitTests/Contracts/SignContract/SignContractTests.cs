namespace EvolutionaryArchitecture.Fitnet.UnitTests.Contracts.SignContract;

using Fitnet.Contracts.Data;
using PrepareContract;
using Shared.SystemClock;

public class SignContractTests
{
    private readonly Mock<ISystemClock> _systemClock = new();

    [Theory]
    [ClassData(typeof(SignContractTestData))]
    internal void Given_sign_contract_Then_expiration_date_is_set_to_contract_duration_from_now(
        DateTimeOffset fakeNow,
        DateTimeOffset signedAt,
        DateTimeOffset preparedAt,
        DateTimeOffset expectedExpirationDate)
    {
        // Arrange
        var contract = PrepareContract(fakeNow, preparedAt);

        // Act
        contract.Sign(signedAt, _systemClock.Object);

        // Assert
        contract.ExpiringAt.Should().Be(expectedExpirationDate);
    }

    private Contract PrepareContract(DateTimeOffset fakeNow, DateTimeOffset preparedAt)
    {
        SetupFakeCurrentTime(fakeNow);
        var prepareContractParameters = PrepareContractParameters.GetValid();
        var contract = Contract.Prepare(
            Guid.NewGuid(),
            prepareContractParameters.MaxAge,
            prepareContractParameters.MaxHeight,
            preparedAt);
        return contract;
    }

    private void SetupFakeCurrentTime(DateTimeOffset fakeNow) =>
        _systemClock.Setup(systemClock => systemClock.Now).Returns(fakeNow);
}