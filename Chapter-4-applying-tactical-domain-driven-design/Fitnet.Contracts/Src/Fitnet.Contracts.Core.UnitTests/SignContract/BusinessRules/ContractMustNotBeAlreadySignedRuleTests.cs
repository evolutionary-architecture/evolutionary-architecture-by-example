namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract.BusinessRules;

using AttachAnnexToBindingContract.BusinessRules;
using Core.SignContract.BusinessRules;

public sealed class ContractMustNotBeAlreadySignedRuleTests
{
    [Fact]
    internal void Given_sign_contract_When_contract_is_already_signed_Then_validation_should_throw()
    {
        // Arrange
        const bool signed = true;

        // Act
        var result = BusinessRuleValidator.Validate(new ContractMustNotBeAlreadySignedRule(signed));

        // Assert
        var error = Error.Validation(nameof(ContractMustNotBeAlreadySignedRule),
            "Contract must not be already signed");
        result.Errors
            .Should()
            .ContainSingle()
            .Which
            .Should()
            .BeEquivalentTo(error);
    }

    [Fact]
    internal void Given_sign_contract_When_contract_is_unsigned_Then_pass_validation()
    {
        // Arrange
        const bool signed = false;

        // Act
        var result =
            BusinessRuleValidator.Validate(
                new ContractMustNotBeAlreadySignedRule(signed));

        // Assert
        result.ShouldBeSuccess();
    }
}
