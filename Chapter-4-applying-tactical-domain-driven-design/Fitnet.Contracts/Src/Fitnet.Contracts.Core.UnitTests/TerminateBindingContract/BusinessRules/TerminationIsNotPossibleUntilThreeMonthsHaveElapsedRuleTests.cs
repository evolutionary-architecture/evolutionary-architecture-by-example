namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.TerminateBindingContract.BusinessRules;

using Common.Core.BusinessRules;
using TerminateContract.BusinessRules;
using TerminationIsNotPossibleUntilThreeMonthsHave.TestData;

public sealed class TerminationIsNotPossibleUntilThreeMonthsHaveElapsedRuleTests
{
    [Theory]
    [ClassData(typeof(BindingContractThreeMonthsHaveNotElapsedTestData))]
    internal void Given_terminate_When_three_months_have_not_elapsed_Then_it_is_not_possible_to_terminate(DateTimeOffset bindingFrom,
        DateTimeOffset terminatedAt)
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new TerminationIsNotPossibleUntilThreeMonthsHaveElapsedRule(bindingFrom, terminatedAt));

        // Assert
        act.Should().Throw<BusinessRuleValidationException>();
    }

    [Theory]
    [ClassData(typeof(BindingContractThreeMonthsHaveElapsedTestData))]
    internal void Given_terminate_When_three_months_have_elapsed_Then_it_is_possible_to_terminate(DateTimeOffset bindingFrom,
        DateTimeOffset terminatedAt)
    {
        // Arrange

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new TerminationIsNotPossibleUntilThreeMonthsHaveElapsedRule(bindingFrom, terminatedAt));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
}
