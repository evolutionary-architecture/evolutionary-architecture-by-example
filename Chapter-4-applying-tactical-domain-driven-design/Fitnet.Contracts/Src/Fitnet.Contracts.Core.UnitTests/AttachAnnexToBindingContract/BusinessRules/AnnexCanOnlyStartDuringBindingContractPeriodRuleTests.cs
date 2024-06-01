namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.AttachAnnexToBindingContract.BusinessRules;

using Core.AttachAnnexToBindingContract.BusinessRules;
using Fitnet.Common.Core.BussinessRules;

public sealed class AnnexCanOnlyStartDuringBindingContractPeriodRuleTests
{
    private readonly DateTimeOffset _now = DateTimeOffset.UtcNow;

    [Fact]
    internal void
        Given_attach_annex_When_annex_valid_from_is_after_binding_contract_expired_Then_it_is_not_possible_to_attach()
    {
        // Arrange
        var bindingContractExpiringAt = _now.AddDays(-1);

        // Act
        var result = BusinessRuleValidator.Validate(
            new AnnexCanOnlyStartDuringBindingContractPeriodRule(
                bindingContractExpiringAt,
                _now));

        // Assert
        var expectedError = BusinessRuleError.Create(nameof(AnnexCanOnlyStartDuringBindingContractPeriodRule),
            "Annex can only start during binding contract period");
        result.Should().ContainError(expectedError);
    }

    [Fact]
    internal void
        Given_attach_annex_When_annex_valid_from_is_before_binding_contract_expired_Then_it_is_possible_to_attach()
    {
        // Arrange
        var bindingContractExpiringAt = _now;

        // Act
        var result =
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyStartDuringBindingContractPeriodRule(
                    bindingContractExpiringAt,
                    _now.AddDays(-1)));

        // Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    internal void
        Given_attach_annex_When_annex_valid_from_is_equal_to_binding_contract_expired_Then_it_is_possible_to_attach()
    {
        // Arrange
        var bindingContractExpiringAt = _now;

        // Act
        var result =
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyStartDuringBindingContractPeriodRule(
                    bindingContractExpiringAt,
                    _now.AddDays(-1)));

        // Assert
        result.Should().BeSuccessful();
    }
}
