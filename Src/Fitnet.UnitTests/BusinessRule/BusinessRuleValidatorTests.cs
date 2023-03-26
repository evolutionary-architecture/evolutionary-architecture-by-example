using SuperSimpleArchitecture.Fitnet.Shared.BusinessRule;

namespace SuperSimpleArchitecture.Fitnet.UnitTests.BusinessRule;

public sealed class BusinessRuleValidatorTests
{
    [Fact]
    public void Given_concrete_business_rule_which_is_met_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new FakeBusinessRule(20));
        
        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
    
    [Fact]
    public void Given_concrete_business_rule_which_is_not_met_Then_validation_should_throw()
    {
        // Arrange
        
        // Act
        var act = () => BusinessRuleValidator.Validate(new FakeBusinessRule(1));
        
        // Assert
        act.Should().Throw<BusinessRuleValidationException>().WithMessage("Fake business rule was not met");
    }
}