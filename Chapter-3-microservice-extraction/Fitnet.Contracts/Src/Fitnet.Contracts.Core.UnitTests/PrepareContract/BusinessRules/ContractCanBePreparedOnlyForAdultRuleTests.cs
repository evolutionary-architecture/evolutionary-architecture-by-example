namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.PrepareContract.BusinessRules;

using Common.Core.BusinessRules;
using Core.PrepareContract.BusinessRules;

public sealed class ContractCanBePreparedOnlyForAdultRuleTests
{
    [Fact]
    internal void Given_customer_age_which_is_less_than_18_Then_validation_should_throw()
    {
        // Arrange
        const int minorAge = 17;

        // Act & Assert
        var exception = Should.Throw<BusinessRuleValidationException>(() =>
            BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(minorAge)));
        exception.Message.ShouldBe("Contract can not be prepared for a person who is not adult");
    }

    [Fact]
    internal void Given_customer_age_which_is_equal_to_18_Then_validation_should_pass()
    {
        // Arrange
        const int exactLegalAge = 18;

        // Act & Assert
        Should.NotThrow(() => BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(exactLegalAge)));
    }

    [Fact]
    internal void Given_customer_age_which_is_greater_than_18_Then_validation_should_pass()
    {
        // Arrange
        const int adultAge = 19;

        // Act & Assert
        Should.NotThrow(() => BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(adultAge)));
    }
}
