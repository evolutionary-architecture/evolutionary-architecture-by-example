namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.PrepareContract.BusinessRules;

using Core.PrepareContract.BusinessRules;
using Fitnet.Common.Core.BussinessRules;

public sealed class ContractCanBePreparedOnlyForAdultRuleTests
{
    [Fact]
    internal void Given_customer_age_which_is_less_than_18_Then_validation_should_have_error()
    {
        // Arrange

        // Act
        var result = BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(17));

        // Assert
        var expectedError = BusinessRuleError.Create(nameof(ContractCanBePreparedOnlyForAdultRule),
            "Contract can not be prepared for a person who is not adult");
        result.Should().ContainError(expectedError);
    }

    [Fact]
    internal void Given_customer_age_which_is_equal_to_18_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var result = BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(18));

        // Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    internal void Given_customer_age_which_is_greater_than_18_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var result = BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(19));

        // Assert
        result.Should().BeSuccessful();
    }
}
