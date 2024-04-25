namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.AttachAnnexToBindingContract.BusinessRules;

using Core.AttachAnnexToBindingContract.BusinessRules;
using EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

public sealed class AnnexCanOnlyStartAfterBindingContractExpirationRuleTests
{
    private readonly DateTimeOffset _now = DateTimeOffset.UtcNow;

    [Fact]
    internal void
        Given_attach_annex_When_annex_valid_from_is_after_binding_contract_expired_Then_it_is_possible_to_attach()
    {
        // Arrange
        var bindingContractExpiringAt = _now.AddDays(-1);

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyStartAfterBindingContractExpirationRule(
                    bindingContractExpiringAt,
                    _now));

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    internal void
        Given_attach_annex_When_annex_valid_from_is_before_binding_contract_expired_Then_it_is_not_possible_to_attach()
    {
        // Arrange
        var bindingContractExpiringAt = _now;

        // Act && Assert
        ShouldThrowException(() =>
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyStartAfterBindingContractExpirationRule(
                    bindingContractExpiringAt,
                    _now.AddDays(-1))));
    }

    [Fact]
    internal void
        Given_attach_annex_When_annex_valid_from_is_equal_to_binding_contract_expired_Then_it_is_not_possible_to_attach()
    {
        // Arrange
        var bindingContractExpiringAt = _now;

        // Act && Assert
        ShouldThrowException(() =>
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyStartAfterBindingContractExpirationRule(
                    bindingContractExpiringAt,
                    _now)));
    }

    private static void ShouldThrowException(Action act) =>
        act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Annex can only start after binding contract expiration");
}
