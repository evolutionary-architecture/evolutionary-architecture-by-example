namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.PrepareContract.BusinessRules;

using Common.Core.BusinessRules;
using Core.PrepareContract.BusinessRules;

public sealed class PreviousContractHasToBeSignedRuleTests
{
    [Fact]
    internal void Given_previous_contract_signed_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new PreviousContractHasToBeSignedRule(true));

        // Assert
        act.ShouldNotThrow();
    }

    [Fact]
    internal void Given_previous_contract_not_exists_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new PreviousContractHasToBeSignedRule(null));

        // Assert
        act.ShouldNotThrow();
    }

    [Fact]
    internal void Given_previous_contract_unsigned_Then_validation_should_throw()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new PreviousContractHasToBeSignedRule(false));

        // Assert
        var exception = act.ShouldThrow<BusinessRuleValidationException>();
        exception.Message.ShouldBe("Previous contract must be signed by the customer");
    }
}
