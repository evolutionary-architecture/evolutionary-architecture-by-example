namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.AttachAnnexToBindingContract.BusinessRules;

using FluentAssertions.Execution;

public static class ErrorOrAssertions
{
    public static AndConstraint<ErrorOr<Success>> ShouldBeSuccess(this ErrorOr<Success> actual)
    {
        Execute.Assertion
            .ForCondition(actual.Value == new Success())
            .FailWith($"Expected Success, but found Error. {string.Join(", ", actual
                .Errors
                .Select(error => error.Description)
                .ToArray())}.");

        return new AndConstraint<ErrorOr<Success>>(actual);
    }
}
