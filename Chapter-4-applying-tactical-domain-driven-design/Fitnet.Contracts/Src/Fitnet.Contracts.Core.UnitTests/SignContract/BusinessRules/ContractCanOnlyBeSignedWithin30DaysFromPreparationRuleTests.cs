namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract.BusinessRules;

using Core.SignContract.BusinessRules;
using Fitnet.Common.Core.BussinessRules;

public sealed class ContractCanOnlyBeSignedWithin30DaysFromPreparationRuleTests
{
    [Fact]
    internal void Given_signed_at_date_which_is_more_than_30_days_from_prepared_at_date_Then_should_have_error()
    {
        // Arrange

        // Act
        var result =
            BusinessRuleValidator.Validate(
                new ContractCanOnlyBeSignedWithin30DaysFromPreparationRule(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(31)));

        // Assert
        var expectedError = BusinessRuleError.Create(nameof(ContractCanOnlyBeSignedWithin30DaysFromPreparationRule),
            "Contract can only be signed within 30 days from preparation");
        result.Should().ContainError(expectedError);
    }

    [Fact]
    internal void Given_signed_at_date_which_is_30_days_from_prepared_at_date_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var result =
            BusinessRuleValidator.Validate(
                new ContractCanOnlyBeSignedWithin30DaysFromPreparationRule(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(30)));

        // Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    internal void Given_signed_at_date_which_is_less_than_30_days_from_prepared_at_date_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var result =
            BusinessRuleValidator.Validate(
                new ContractCanOnlyBeSignedWithin30DaysFromPreparationRule(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(29)));

        // Assert
        result.Should().BeSuccessful();
    }
}
