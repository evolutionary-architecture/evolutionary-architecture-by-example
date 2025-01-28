namespace EvolutionaryArchitecture.Fitnet.UnitTests.Contracts.SignContract.BusinessRules;

using EvolutionaryArchitecture.Fitnet.Contracts.SignContract.BusinessRules;
using EvolutionaryArchitecture.Fitnet.Common.BusinessRulesEngine;

public sealed class ContractCanOnlyBeSignedWithin30DaysFromPreparationTests
{
    [Fact]
    internal void Given_signed_at_date_which_is_more_than_30_days_from_prepared_at_date_Then_validation_should_throw()
    {
        // Arrange
        var preparedAt = DateTimeOffset.Now;
        var signedAt = DateTimeOffset.Now.AddDays(31);

        // Act
        var exception = Should.Throw<BusinessRuleValidationException>(() => BusinessRuleValidator.Validate(new ContractCanOnlyBeSignedWithin30DaysFromPreparation(preparedAt, signedAt)));

        // Assert
        exception.Message.ShouldBe("Contract can not be signed because more than 30 days have passed from the contract preparation");
    }

    [Fact]
    internal void Given_signed_at_date_which_is_30_days_from_prepared_at_date_Then_validation_should_pass()
    {
        // Arrange
        var preparedAt = DateTimeOffset.Now;
        var signedAt = DateTimeOffset.Now.AddDays(30);

        // Act
        // Assert
        Should.NotThrow(() => BusinessRuleValidator.Validate(new ContractCanOnlyBeSignedWithin30DaysFromPreparation(preparedAt, signedAt)));
    }

    [Fact]
    internal void Given_signed_at_date_which_is_less_than_30_days_from_prepared_at_date_Then_validation_should_pass()
    {
        // Arrange
        var preparedAt = DateTimeOffset.Now;
        var signedAt = DateTimeOffset.Now.AddDays(29);

        // Act & Assert
        Should.NotThrow(() => BusinessRuleValidator.Validate(new ContractCanOnlyBeSignedWithin30DaysFromPreparation(preparedAt, signedAt)));
    }
}
