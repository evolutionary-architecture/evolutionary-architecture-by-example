namespace EvolutionaryArchitecture.Fitnet.Common.Core.UnitTests.BusinessRulesEngine;

using BusinessRules;
using Shouldly;

public sealed class BusinessRuleValidatorTests
{
    [Fact]
    internal void Given_concrete_business_rule_which_is_met_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new FakeBusinessRule(20));

        // Assert
        act.ShouldNotThrow<BusinessRuleValidationException>();
    }

    [Fact]
    internal void Given_concrete_business_rule_which_is_not_met_Then_validation_should_throw()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new FakeBusinessRule(1));

        // Assert
        act.ShouldThrow<BusinessRuleValidationException>().WithMessage("Fake business rule was not met");
    }
}
