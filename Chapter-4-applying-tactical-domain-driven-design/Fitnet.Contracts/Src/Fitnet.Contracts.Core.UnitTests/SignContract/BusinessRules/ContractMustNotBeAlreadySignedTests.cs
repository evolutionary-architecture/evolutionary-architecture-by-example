namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract.BusinessRules;

using Core.SignContract.BusinessRules;
using EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

public sealed class ContractMustNotBeAlreadySignedTests
{
    [Fact]
    internal void Given_unsigned_contract_and_attempt_to_sign_When_contract_is_already_signed_Then_validation_should_throw()
    {
        // Arrange
        var signed = true;

        // Act
        var act = () => BusinessRuleValidator.Validate(new ContractMustNotBeAlreadySigned(signed));

        // Assert
        act.Should()
            .Throw<BusinessRuleValidationException>()
            .WithMessage("Contract is already signed");
    }

    [Fact]
    internal void Given_unsigned_contract_and_attempt_to_sign_Then_pass_validation()
    {
        // Arrange
        var signed = false;

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new ContractMustNotBeAlreadySigned(signed));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
}
