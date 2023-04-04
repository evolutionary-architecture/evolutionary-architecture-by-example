using SuperSimpleArchitecture.Fitnet.Contracts.PrepareContract.BusinessRules;
using SuperSimpleArchitecture.Fitnet.Shared.BusinessRulesEngine;

namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Contracts.PrepareContract.BusinessRules;

public sealed class ContractCanBePreparedOnlyForAdultRuleTests
{
    [Fact]
    public void Given_customer_age_which_is_less_than_18_Then_validation_should_throw()
    {
        // Arrange
        
        // Act
        var act = () => BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(17));
        
        // Assert
        act.Should().Throw<BusinessRuleValidationException>().WithMessage("Contract can not be prepared for a person who is not adult");
    }
    
    [Fact]
    public void Given_customer_age_which_is_equal_to_18_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(18));
        
        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
    
    [Fact]
    public void Given_customer_age_which_is_greater_than_18_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(19));
        
        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
}