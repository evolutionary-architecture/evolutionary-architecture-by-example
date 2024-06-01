namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract.BusinessRules;

using Core.SignContract.BusinessRules;
using Fitnet.Common.Core.BussinessRules;

public sealed class ContractMustNotBeAlreadySignedRuleTests
{
    [Fact]
    internal void Given_sign_contract_When_contract_is_already_signed_Then_validation_should_have_error()
    {
        // Arrange
        const bool signed = true;

        // Act
        var result = BusinessRuleValidator.Validate(new ContractMustNotBeAlreadySignedRule(signed));

        // Assert
        var expectedError = BusinessRuleError.Create(nameof(ContractMustNotBeAlreadySignedRule),
            "Contract must not be already signed");
        result.Should().ContainError(expectedError);
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
        result.Should().BeSuccessful();
    }
}
