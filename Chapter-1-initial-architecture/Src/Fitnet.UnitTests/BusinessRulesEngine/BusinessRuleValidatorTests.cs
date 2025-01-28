namespace EvolutionaryArchitecture.Fitnet.UnitTests.BusinessRulesEngine;

using EvolutionaryArchitecture.Fitnet.Common.BusinessRulesEngine;

public sealed class BusinessRuleValidatorTests
{
    [Fact]
    internal void Given_concrete_business_rule_which_is_met_Then_validation_should_pass() =>
        // Arrange
        // Act & Assert
        Should.NotThrow(() => BusinessRuleValidator.Validate(new FakeBusinessRule(20)));

    [Fact]
    internal void Given_concrete_business_rule_which_is_not_met_Then_validation_should_throw()
    {
        // Arrange

        // Act
        var exception =
            Should.Throw<BusinessRuleValidationException>(() =>
                BusinessRuleValidator.Validate(new FakeBusinessRule(1)));

        //  Assert
        exception.Message.ShouldBe("Fake business rule was not met");
    }
}
