namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.Common.Assertions.ErrorOr;

using FluentAssertions.Execution;
using FluentAssertions.Primitives;

internal sealed class ErrorOrSuccessAssertions(ErrorOr<Success> subject)
    : ReferenceTypeAssertions<ErrorOr<Success>, ErrorOrSuccessAssertions>(subject)
{

    protected override string Identifier => "ErrorOr<Success>";

    public AndConstraint<ErrorOr<Success>> BeSuccessful(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(!Subject.IsError)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:ErrorOr<Success>} to be successful{reason}, but found {0}.", string.Join(", ", Subject.Errors.Select(x => x.Description)));

        return new AndConstraint<ErrorOr<Success>>(Subject);
    }

    public AndConstraint<ErrorOr<Success>> ContainError(Error error, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(Subject.IsError && Subject.Errors.Contains(error))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected to contain error '{0}' but found errors: {1}", string.Join(", ", Subject.Errors.Select(x => x.Description)), error.Description);

        return new AndConstraint<ErrorOr<Success>>(Subject);
    }
}
