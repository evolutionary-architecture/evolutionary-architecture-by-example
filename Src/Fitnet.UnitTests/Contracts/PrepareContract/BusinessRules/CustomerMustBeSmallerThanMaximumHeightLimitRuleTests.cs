using SuperSimpleArchitecture.Fitnet.Contracts.PrepareContract.BusinessRules;
using SuperSimpleArchitecture.Fitnet.Shared.BusinessRulesEngine;

namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Contracts.PrepareContract.BusinessRules;

public sealed class CustomerMustBeSmallerThanMaximumHeightLimitRuleTests
{
    [Fact]
    public void Given_customer_height_which_is_greater_than_maximum_height_limit_Then_validation_should_throw()
    {
        // Arrange
        const int height = 211;

        // Act
        var act = () => BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(height));
        
        // Assert
        act.Should().Throw<BusinessRuleValidationException>().WithMessage("Customer height must fit maximum limit for gym instruments");
    }
    
    [Fact]
    public void Given_customer_height_which_is_equal_to_maximum_height_limit_Then_validation_should_pass()
    {
        // Arrange
        const int height = 210;

        // Act
        var act = () => BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(height));
        
        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
    
    [Fact]
    public void Given_customer_height_which_is_less_than_maximum_height_limit_Then_validation_should_pass()
    {
        // Arrange
        const int height = 209;

        // Act
        var act = () => BusinessRuleValidator.Validate(new CustomerMustBeSmallerThanMaximumHeightLimitRule(height));
        
        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
}