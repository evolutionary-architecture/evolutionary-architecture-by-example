namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract;

using PrepareContract;

public class SignContractTests
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
        var contract = PrepareContract(preparedAt);

        // Act
        var bindingContract = contract.Sign(signedAt, fakeNow);

        // Assert
        bindingContract.ExpiringAt.Should().Be(expectedExpirationDate);
    }

    private static readonly DateTimeOffset PreparedAt = new(2023, 1, 1, 0, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset FakeNow = PreparedAt.AddDays(1);
    private static readonly DateTimeOffset SignedAt = PreparedAt.AddDays(1);

    [Fact]
    internal void Given_sign_contract_Then_contracts_becomes_binding_contract()
    {
        // Arrange
        var contract = PrepareContract(PreparedAt);

        // Act
        var bindingContract = contract.Sign(SignedAt, FakeNow);

        // Assert
        bindingContract.Should().NotBeNull();
        bindingContract.Should().BeOfType<BindingContract>();
    }

    [Fact]
    internal void Given_sign_contract_When_contract_is_already_signed_Then_throws_contract_already_signed_exception()
    {
        // Arrange
        var contract = PrepareContract(PreparedAt);
        contract.Sign(SignedAt, FakeNow);

        // Act
        var act = () => contract.Sign(SignedAt, FakeNow);

        // Assert
        act.Should().Throw<ContractAlreadySignedException>().WithMessage(
            "Contract is already signed");
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
