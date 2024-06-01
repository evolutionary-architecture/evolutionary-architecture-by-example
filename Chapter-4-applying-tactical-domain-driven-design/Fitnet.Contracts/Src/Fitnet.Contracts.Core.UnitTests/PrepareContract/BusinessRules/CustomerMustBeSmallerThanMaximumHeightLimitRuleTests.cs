namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.PrepareContract.BusinessRules;

using Core.PrepareContract.BusinessRules;
using Fitnet.Common.Core.BussinessRules;

public sealed class CustomerMustBeSmallerThanMaximumHeightLimitRuleTests
{
    [Fact]
    internal void Given_customer_height_which_is_greater_than_maximum_height_limit_Then_validation_should_have_error()
    {
        // Arrange
        const int height = 211;

        // Act
        var result = BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(height));

        // Assert
        var expectedError = BusinessRuleError.Create(nameof(CustomerMustBeSmallerThanMaximumHeightLimitRule), "Customer height must fit maximum limit for gym instruments");
        result.Should().ContainError(expectedError);
    }

    [Fact]
    internal void Given_customer_height_which_is_equal_to_maximum_height_limit_Then_validation_should_pass()
    {
        // Arrange
        const int height = 210;

        // Act
        var result = BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(height));

        // Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    internal void Given_customer_height_which_is_less_than_maximum_height_limit_Then_validation_should_pass()
    {
        // Arrange
        const int height = 209;

        // Act
        var result = BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(height));

        // Assert
        result.Should().BeSuccessful();
    }
}
