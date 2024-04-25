namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.AttachAnnexToBindingContract.BusinessRules;

using Core.AttachAnnexToBindingContract.BusinessRules;
using EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

public sealed class AnnexCanOnlyStartDuringBindingContractPeriodRuleTests
{
    private readonly DateTimeOffset _now = DateTimeOffset.UtcNow;

    [Fact]
    internal void
        Given_attach_annex_When_annex_valid_from_is_after_binding_contract_expired_Then_it_is_not_possible_to_attach()
    {
        // Arrange
        var bindingContractExpiringAt = _now.AddDays(-1);

        // Act && Assert
        ShouldThrowException(() =>
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyStartDuringBindingContractPeriodRule(
                    bindingContractExpiringAt,
                    _now)));
    }

    [Fact]
    internal void
        Given_attach_annex_When_annex_valid_from_is_before_binding_contract_expired_Then_it_is_possible_to_attach()
    {
        // Arrange
        var bindingContractExpiringAt = _now;

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyStartDuringBindingContractPeriodRule(
                    bindingContractExpiringAt,
                    _now.AddDays(-1)));

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    internal void
        Given_attach_annex_When_annex_valid_from_is_equal_to_binding_contract_expired_Then_it_is_possible_to_attach()
    {
        // Arrange
        var bindingContractExpiringAt = _now;

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyStartDuringBindingContractPeriodRule(
                    bindingContractExpiringAt,
                    _now.AddDays(-1)));

        // Assert
        act.Should().NotThrow();
    }

    private static void ShouldThrowException(Action act) =>
        act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Annex can only start during binding contract period");
}
