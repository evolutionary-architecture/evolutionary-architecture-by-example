namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Common;

using ErrorOr;
using Microsoft.AspNetCore.Http;

internal static class ErrorResults
{
    internal static IResult ToProblem(this IReadOnlyCollection<Error> errors)
    {
        var error = errors.First();
        return error.ToProblem();
    }

    private static IResult ToProblem(this Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        return Results.Problem(error.Description, statusCode: statusCode);
    }
}
