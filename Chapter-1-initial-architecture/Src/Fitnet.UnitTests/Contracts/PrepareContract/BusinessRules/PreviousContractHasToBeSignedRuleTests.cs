namespace EvolutionaryArchitecture.Fitnet.UnitTests.Contracts.PrepareContract.BusinessRules;

using Fitnet.Contracts.PrepareContract.BusinessRules;
using EvolutionaryArchitecture.Fitnet.Common.BusinessRulesEngine;

public sealed class PreviousContractHasToBeSignedRuleTests
{
    [Fact]
    internal void Given_previous_contract_signed_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new PreviousContractHasToBeSignedRule(true));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
    
    [Fact]
    internal void Given_previous_contract_not_exists_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new PreviousContractHasToBeSignedRule(null));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }


    [Fact]
    internal void Given_previous_contract_unsigned_Then_validation_should_throw()
    {
        // Arrange

        // Act
        var act = () => BusinessRuleValidator.Validate(new PreviousContractHasToBeSignedRule(false));

        // Assert
        act.Should().Throw<BusinessRuleValidationException>().WithMessage("Previous contract must be signed by the customer");
    }
}