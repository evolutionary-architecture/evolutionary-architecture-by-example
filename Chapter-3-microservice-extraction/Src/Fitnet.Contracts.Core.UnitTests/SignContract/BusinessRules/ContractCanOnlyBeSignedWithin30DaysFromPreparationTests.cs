namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract.BusinessRules;

using Common.Core.BusinessRules;
using Core.SignContract.BusinessRules;

public sealed class ContractCanOnlyBeSignedWithin30DaysFromPreparationTests
{
    [Fact]
    internal void Given_signed_at_date_which_is_more_than_30_days_from_prepared_at_date_Then_validation_should_throw()
    {
        // Arrange
        
        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new ContractCanOnlyBeSignedWithin30DaysFromPreparation(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(31)));
        
        // Assert
        act.Should().Throw<BusinessRuleValidationException>().WithMessage(
            "Contract can not be signed because more than 30 days have passed from the contract preparation");
    }
    
    [Fact]
    internal void Given_signed_at_date_which_is_30_days_from_prepared_at_date_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new ContractCanOnlyBeSignedWithin30DaysFromPreparation(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(30)));
        
        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
    
    [Fact]
    internal void Given_signed_at_date_which_is_less_than_30_days_from_prepared_at_date_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new ContractCanOnlyBeSignedWithin30DaysFromPreparation(DateTimeOffset.Now,
                    DateTimeOffset.Now.AddDays(29)));
        
        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
}