namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.AttachAnnexToBindingContract.BusinessRules;

using Core.AttachAnnexToBindingContract.BusinessRules;
using FluentAssertions.Execution;

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
        var error = Error.Validation(nameof(AnnexCanOnlyStartDuringBindingContractPeriodRule),
            "Annex can only start during binding contract period");
        result.Errors
            .Should()
                .ContainSingle()
            .Which
            .Should()
                .BeEquivalentTo(error);
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
        result.ShouldBeSuccess();
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
        result.ShouldBeSuccess();
    }
}

public static class ErrorOrAssertions
{
    public static AndConstraint<ErrorOr<Success>> ShouldBeSuccess(this ErrorOr<Success> actual)
    {
        var errors = actual
            .Errors
            .Select(error => error.Description)
            .ToArray();
        var errorsString = string.Join(", ", errors);

        Execute.Assertion
            .ForCondition(actual.Value == new Success() && actual.Errors.Count == 0)
            .FailWith($"Expected Success, but found Error. {errorsString}.");

        return new AndConstraint<ErrorOr<Success>>(actual);
    }
}
