namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.TerminateBindingContract.BusinessRules;

using BindingContract3MonthsNotPassed.TestData;
using Common.Core.BusinessRules;

public sealed class TerminationCannotBeBefore3MonthsFromStartBindingContractTests
{
    [Theory]
    [ClassData(typeof(BindingContract3MonthsNotPassedTestData))]
    internal void Given_terminate_When_3_months_not_passed_Then_it_is_not_possible_to_terminate(DateTimeOffset signDate,
        DateTimeOffset nowDate)
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new TerminationCannotBeBefore3MonthsFromStartBindingContract(signDate, nowDate));

        // Assert
        act.Should().Throw<BusinessRuleValidationException>();
    }

    [Theory]
    [ClassData(typeof(BindingContract3MonthsPassedTestData))]
    internal void Given_terminate_When_3_months_passed_Then_it_is_possible_to_terminate(DateTimeOffset signDate,
        DateTimeOffset nowDate)
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new TerminationCannotBeBefore3MonthsFromStartBindingContract(signDate, nowDate));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
}
