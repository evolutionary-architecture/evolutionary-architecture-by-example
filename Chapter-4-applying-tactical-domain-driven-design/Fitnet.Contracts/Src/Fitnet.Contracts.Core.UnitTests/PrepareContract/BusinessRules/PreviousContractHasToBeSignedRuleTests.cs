namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.PrepareContract.BusinessRules;

using Common.Assertions;
using Core.PrepareContract.BusinessRules;

public sealed class PreviousContractHasToBeSignedRuleTests
{
    [Fact]
    internal void Given_previous_contract_signed_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var result = BusinessRuleValidator.Validate(new PreviousContractHasToBeSignedRule(true));

        // Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    internal void Given_previous_contract_not_exists_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var result = BusinessRuleValidator.Validate(new PreviousContractHasToBeSignedRule(null));

        // Assert
        result.Should().BeSuccessful();
    }


    [Fact]
    internal void Given_previous_contract_unsigned_Then_should_have_error()
    {
        // Arrange

        // Act
        var result = BusinessRuleValidator.Validate(new PreviousContractHasToBeSignedRule(false));

        // Assert
        var expectedError = Error.Validation(nameof(PreviousContractHasToBeSignedRule), "Previous contract has to be signed");
        result.Should().ContainError(expectedError);
    }
}
