namespace EvolutionaryArchitecture.Fitnet.Common.Core.UnitTests.BusinessRulesEngine;

using BusinessRules;

public sealed class BusinessRuleValidatorTests
{
    [Fact]
    internal void Given_concrete_business_rule_which_is_met_Then_validation_should_pass()
    {
        // Arrange

        // Act
        static void FakeBusinessRule()
        {
            BusinessRuleValidator.Validate(new FakeBusinessRule(20));
        }

        // Assert
        Should.NotThrow(FakeBusinessRule);
    }

    [Fact]
    internal void Given_concrete_business_rule_which_is_not_met_Then_validation_should_throw()
    {
        // Arrange

        // Act
        static void NotMetRuleAction()
        {
            BusinessRuleValidator.Validate(new FakeBusinessRule(1));
        }

        // Assert
        var exception = Should.Throw<BusinessRuleValidationException>(NotMetRuleAction);
        exception.Message.ShouldBe("Fake business rule was not met");
    }
}
