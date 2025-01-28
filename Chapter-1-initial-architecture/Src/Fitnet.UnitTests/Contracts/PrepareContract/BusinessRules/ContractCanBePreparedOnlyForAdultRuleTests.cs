namespace EvolutionaryArchitecture.Fitnet.UnitTests.Contracts.PrepareContract.BusinessRules;

using EvolutionaryArchitecture.Fitnet.Contracts.PrepareContract.BusinessRules;
using EvolutionaryArchitecture.Fitnet.Common.BusinessRulesEngine;


public sealed class ContractCanBePreparedOnlyForAdultRuleTests
{
    [Fact]
    internal void Given_customer_age_which_is_less_than_18_Then_validation_should_throw()
    {
        // Arrange

        // Act & Assert
        var exception = Should.Throw<BusinessRuleValidationException>(() => BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(17)));
        exception.Message.ShouldBe("Contract can not be prepared for a person who is not adult");
    }

    [Fact]
    internal void Given_customer_age_which_is_equal_to_18_Then_validation_should_pass() =>
        // Arrange

        // Act & Assert
        Should.NotThrow(() => BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(18)));

    [Fact]
    internal void Given_customer_age_which_is_greater_than_18_Then_validation_should_pass() =>
        // Arrange

        // Act & Assert
        Should.NotThrow(() => BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(19)));
}
