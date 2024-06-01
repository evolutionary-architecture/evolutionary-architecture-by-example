namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.AttachAnnexToBindingContract.BusinessRules;

using Core.AttachAnnexToBindingContract.BusinessRules;
using Fitnet.Common.Core.BussinessRules;

public sealed class AnnexCanOnlyBeAttachedToActiveBindingContractRuleTests
{
    private readonly DateTimeOffset _now = DateTimeOffset.UtcNow;

    [Fact]
    internal void Given_attach_annex_When_binding_contract_terminated_Then_it_is_not_possible_to_attach()
    {
        // Arrange
        var bindingContractTerminatedAt = _now.AddDays(-1);
        var bindingContractExpiringAt = _now.AddYears(1);

        // Act
        var result = BusinessRuleValidator.Validate(
            new AnnexCanOnlyBeAttachedToActiveBindingContractRule(
                bindingContractTerminatedAt,
                bindingContractExpiringAt,
                _now));

        // Assert
        var expectedError = BusinessRuleError.Create(nameof(AnnexCanOnlyBeAttachedToActiveBindingContractRule),
            "Annex can only be attached to active binding contract");
        result.Should().ContainError(expectedError);
    }

    [Fact]
    internal void Given_attach_annex_When_binding_contract_expired_Then_it_is_not_possible_to_attach()
    {
        // Arrange
        var bindingContractExpiringAt = _now.AddDays(-1);

        // Act
        var result = BusinessRuleValidator.Validate(
            new AnnexCanOnlyBeAttachedToActiveBindingContractRule(
                null,
                bindingContractExpiringAt,
                _now));

        // Assert
        var expectedError = BusinessRuleError.Create(nameof(AnnexCanOnlyBeAttachedToActiveBindingContractRule),
            "Annex can only be attached to active binding contract");
        result.Should().ContainError(expectedError);
    }

    [Fact]
    internal void Given_attach_annex_When_binding_contract_expired_and_terminated_Then_it_is_not_possible_to_attach()
    {
        // Arrange
        var bindingContractTerminatedAt = _now.AddDays(-1);
        var bindingContractExpiringAt = _now.AddDays(-1);

        // Act && Assert
        var result = BusinessRuleValidator.Validate(
                new AnnexCanOnlyBeAttachedToActiveBindingContractRule(
                    bindingContractTerminatedAt,
                    bindingContractExpiringAt,
                    _now));

        var expectedError = BusinessRuleError.Create(nameof(AnnexCanOnlyBeAttachedToActiveBindingContractRule),
            "Annex can only be attached to active binding contract");
        result.Should().ContainError(expectedError);
    }

    [Fact]
    internal void Given_attach_annex_When_binding_contract_is_not_expired_and_is_not_terminated_Then_it_is_possible_to_attach()
    {
        // Arrange
        var bindingContractExpiringAt = _now.AddYears(1);

        // Act
        var result =
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyBeAttachedToActiveBindingContractRule(
                    null,
                    bindingContractExpiringAt,
                    _now));

        // Assert
        result.Should().BeSuccessful();
    }
}
