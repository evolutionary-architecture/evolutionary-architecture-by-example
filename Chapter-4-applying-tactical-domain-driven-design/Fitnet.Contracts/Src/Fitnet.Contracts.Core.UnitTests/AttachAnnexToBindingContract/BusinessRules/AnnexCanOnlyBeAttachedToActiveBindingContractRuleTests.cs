namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.AttachAnnexToBindingContract.BusinessRules;

using Core.AttachAnnexToBindingContract.BusinessRules;
using EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

public sealed class AnnexCanOnlyBeAttachedToActiveBindingContractRuleTests
{
    [Fact]
    internal void Given_attach_annex_When_binding_contract_terminated_Then_it_is_not_possible_to_attach()
    {
        // Arrange
        var now = DateTimeOffset.UtcNow;
        var bindingContractTerminatedAt = now.AddDays(-1);
        var bindingContractExpiringAt = now.AddYears(1);

        // Act && Assert
        ShouldThrowException(() =>
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyBeAttachedToActiveBindingContractRule(
                    bindingContractTerminatedAt,
                    bindingContractExpiringAt,
                    now)));
    }

    [Fact]
    internal void Given_attach_annex_When_binding_contract_expired_Then_it_is_not_possible_to_attach()
    {
        // Arrange
        var now = DateTimeOffset.UtcNow;
        var bindingContractExpiringAt = now.AddDays(-1);

        // Act && Assert
        ShouldThrowException(() =>
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyBeAttachedToActiveBindingContractRule(
                    null,
                    bindingContractExpiringAt,
                    now)));
    }

    [Fact]
    internal void Given_attach_annex_When_binding_contract_expired_and_terminated_Then_it_is_not_possible_to_attach()
    {
        // Arrange
        var now = DateTimeOffset.UtcNow;
        var bindingContractTerminatedAt = now.AddDays(-1);
        var bindingContractExpiringAt = now.AddDays(-1);

        // Act && Assert
        ShouldThrowException(() =>
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyBeAttachedToActiveBindingContractRule(
                    bindingContractTerminatedAt,
                    bindingContractExpiringAt,
                    now)));
    }

    [Fact]
    internal void Given_attach_annex_When_binding_contract_is_not_expired_and_is_not_terminated_Then_it_is_possible_to_attach()
    {
        // Arrange
        var now = DateTimeOffset.UtcNow;
        var bindingContractExpiringAt = now.AddYears(1);

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyBeAttachedToActiveBindingContractRule(
                    null,
                    bindingContractExpiringAt,
                    now));

        // Assert
        act.Should().NotThrow();
    }

    private static void ShouldThrowException(Action act) =>
        act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Annex can only be attached to active binding contract");
}
