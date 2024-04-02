namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.AddAnnex.BusinessRules;

using Common.Core.BusinessRules;
using Core.AddAnnex.BusinessRules;

public sealed class AnnexCanOnlyBeAddedOnlyBeAddedToActiveBindingContractRuleTests
{
    [Fact]
    internal void Given_add_annex_When_binding_contract_terminated_Then_it_is_not_possible_to_add()
    {
        // Arrange
        var now = DateTimeOffset.UtcNow;
        var terminatedAt = now.AddDays(-1);
        var expiringAt = now.AddYears(1);

        // Act && Assert
        ShouldThrowException(() =>
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyBeAddedOnlyBeAddedToActiveBindingContractRule(terminatedAt, expiringAt, now)));
    }

    [Fact]
    internal void Given_add_annex_When_binding_contract_expired_Then_it_is_not_possible_to_add()
    {
        // Arrange
        var now = DateTimeOffset.UtcNow;
        var expiringAt = now.AddDays(-1);

        // Act && Assert
        ShouldThrowException(() =>
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyBeAddedOnlyBeAddedToActiveBindingContractRule(null, expiringAt, now)));
    }

    [Fact]
    internal void Given_add_annex_When_binding_contract_expired_and_terminated_Then_it_is_not_possible_to_add()
    {
        // Arrange
        var now = DateTimeOffset.UtcNow;
        var terminatedAt = now.AddDays(-1);
        var expiringAt = now.AddDays(-1);

        // Act && Assert
        ShouldThrowException(() =>
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyBeAddedOnlyBeAddedToActiveBindingContractRule(terminatedAt, expiringAt, now)));
    }

    [Fact]
    internal void Given_add_annex_When_binding_contract_is_not_expired_and_is_not_terminated_Then_it_is_possible_to_add()
    {
        // Arrange
        var now = DateTimeOffset.UtcNow;
        var expiringAt = now.AddYears(1);

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new AnnexCanOnlyBeAddedOnlyBeAddedToActiveBindingContractRule(null, expiringAt, now));

        // Assert
        act.Should().NotThrow();
    }

    private static void ShouldThrowException(Action act) =>
        act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Annex can only be added to active binding contract");
}
