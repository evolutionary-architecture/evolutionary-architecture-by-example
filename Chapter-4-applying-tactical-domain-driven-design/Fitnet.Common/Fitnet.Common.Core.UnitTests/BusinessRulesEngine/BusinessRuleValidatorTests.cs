namespace EvolutionaryArchitecture.Fitnet.Common.Core.UnitTests.BusinessRulesEngine;

using BussinessRules;

public sealed class BusinessRuleValidatorTests
{
    [Fact]
    internal void Given_concrete_business_rule_which_is_met_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var result = BusinessRuleValidator.Validate(new FakeBusinessRule(20));

        // Assert
        result.IsError.ShouldBeFalse();
    }

    [Fact]
    internal void Given_concrete_business_rule_which_is_not_met_Then_validation_should_throw()
    {
        // Arrange
        var testRule = new FakeBusinessRule(1);

        // Act
        var result = BusinessRuleValidator.Validate(testRule);

        // Assert
        result.Errors.ShouldContain(testRule.Error);
    }
}
