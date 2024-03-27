namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract.BusinessRules;

using Core.SignContract.BusinessRules;
using EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

public sealed class ContractMustNotBeAlreadySignedRuleTests
{
    [Fact]
    internal void Given_sign_contract_When_contract_is_already_signed_Then_validation_should_throw()
    {
        // Arrange
        const bool signed = true;

        // Act
        var act = () => BusinessRuleValidator.Validate(new ContractMustNotBeAlreadySignedRule(signed));

        // Assert
        act.Should()
            .Throw<BusinessRuleValidationException>()
            .WithMessage("Contract is already signed");
    }

    [Fact]
    internal void Given_sign_contract_When_contract_is_unsigned_Then_pass_validation()
    {
        // Arrange
        const bool signed = false;

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new ContractMustNotBeAlreadySignedRule(signed));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
}
