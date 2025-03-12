namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract.BusinessRules;

using Common.Core.BusinessRules;
using Core.SignContract.BusinessRules;

public sealed class ContractCanOnlyBeSignedWithin30DaysFromPreparationTests
{
    [Fact]
    internal void Given_signed_at_date_which_is_more_than_30_days_from_prepared_at_date_Then_validation_should_throw()
    {
        // Arrange & Act & Assert
        var exception = Should.Throw<BusinessRuleValidationException>(() =>
            BusinessRuleValidator.Validate(
                new ContractCanOnlyBeSignedWithin30DaysFromPreparation(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(31))));

        exception.Message.ShouldBe(
            "Contract can not be signed because more than 30 days have passed from the contract preparation");
    }

    [Fact]
    internal void Given_signed_at_date_which_is_30_days_from_prepared_at_date_Then_validation_should_pass() =>
        // Arrange & Act & Assert
        Should.NotThrow(() =>
            BusinessRuleValidator.Validate(
                new ContractCanOnlyBeSignedWithin30DaysFromPreparation(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(30))));

    [Fact]
    internal void Given_signed_at_date_which_is_less_than_30_days_from_prepared_at_date_Then_validation_should_pass() =>
        // Arrange & Act & Assert
        Should.NotThrow(() =>
            BusinessRuleValidator.Validate(
                new ContractCanOnlyBeSignedWithin30DaysFromPreparation(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(29))));
}
