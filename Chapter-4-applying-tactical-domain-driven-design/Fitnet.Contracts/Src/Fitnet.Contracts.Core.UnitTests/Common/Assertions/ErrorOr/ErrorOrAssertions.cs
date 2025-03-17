namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.Common.Assertions.ErrorOr;

using System.Linq;

public static class ErrorOrSuccessAssertions
{
    public static void ShouldBeSuccessful(this ErrorOr<Success> errorOr, string? message = null)
    {
        if (errorOr.IsError)
        {
            var errorMessage = string.Join(", ", errorOr.Errors.Select(x => x.Description));
            throw new ShouldAssertException(message ?? $"ErrorOr<Success> should be successful but found errors: {errorMessage}");
        }
    }

    public static void ShouldContainError(this ErrorOr<Success> errorOr, Error error, string? message = null)
    {
        errorOr.IsError.ShouldBeTrue("ErrorOr<Success> should be in error state");
        if (!errorOr.Errors.Contains(error))
        {
            var actualErrors = string.Join(", ", errorOr.Errors.Select(x => x.Description));
            throw new ShouldAssertException(message ?? $"Expected to contain error '{error.Description}' but found errors: {actualErrors}");
        }
    }
}
