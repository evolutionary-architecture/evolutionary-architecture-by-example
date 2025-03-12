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

        // Act & Assert
        var exception = Should.Throw<BusinessRuleValidationException>(() =>
            BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(height)));

        exception.Message.ShouldBe("Customer height must fit maximum limit for gym instruments");
    }

    [Fact]
    internal void Given_customer_height_which_is_equal_to_maximum_height_limit_Then_validation_should_pass()
    {
        // Arrange
        const int height = 210;

        // Act & Assert
        Should.NotThrow(() => BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(height)));
    }

    [Fact]
    internal void Given_customer_height_which_is_less_than_maximum_height_limit_Then_validation_should_pass()
    {
        // Arrange
        const int height = 209;

        // Act & Assert
        Should.NotThrow(() => BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(height)));
    }
}
