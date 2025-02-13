namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.PrepareContract.BusinessRules;

using Common.Core.BusinessRules;
using Core.PrepareContract.BusinessRules;

public sealed class CustomerMustBeSmallerThanMaximumHeightLimitRuleTests
{
    [Fact]
    internal void Given_customer_height_which_is_greater_than_maximum_height_limit_Then_validation_should_throw()
    {
        // Arrange
        const int height = 211;

        // Act
        var act = () => BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(height));

        // Assert
        var exception = act.ShouldThrow<BusinessRuleValidationException>();
        exception.Message.ShouldBe("Customer height must fit maximum limit for gym instruments");
    }

    [Fact]
    internal void Given_customer_height_which_is_equal_to_maximum_height_limit_Then_validation_should_pass()
    {
        // Arrange
        const int height = 210;

        // Act
        var act = () => BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(height));

        // Assert
        act.ShouldNotThrow();
    }

    [Fact]
    internal void Given_customer_height_which_is_less_than_maximum_height_limit_Then_validation_should_pass()
    {
        // Arrange
        const int height = 209;

        // Act
        var act = () => BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(height));

        // Assert
        act.ShouldNotThrow();
    }
}
