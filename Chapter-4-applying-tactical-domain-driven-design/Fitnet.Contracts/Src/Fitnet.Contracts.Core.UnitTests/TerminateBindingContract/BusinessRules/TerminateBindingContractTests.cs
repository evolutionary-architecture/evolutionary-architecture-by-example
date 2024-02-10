namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.TerminateBindingContract.BusinessRules;

using Common.Core.BusinessRules;

public sealed class TerminateBindingContractTests
{
    [Fact]
    internal void Given_terminate_whenever_3_months_passed_it_is_possible_to_terminate()
    {
        // Arrange
        var signDate = new DateTimeOffset(2022, 3, 4, 5, 1, 3, TimeSpan.Zero);
        var nowDate = new DateTimeOffset(2022, 9, 10, 5, 1, 3, TimeSpan.Zero);

        // Act
        var act = () =>
            BusinessRuleValidator.Validate(
                new TerminationCannotBeBefore3MonthsFromStartBindingContract(signDate, nowDate));

        // Assert
        act.Should().NotThrow<BusinessRuleValidationException>();
    }
}
