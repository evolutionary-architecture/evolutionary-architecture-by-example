namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Common.Errors;

using Core.Common.BussinessRules;
using Microsoft.AspNetCore.Http;
using static ErrorOr.ErrorType;

internal static class ProblemResults
{
    internal static IResult ToProblem(this IReadOnlyCollection<Error> errors)
    {
        var error = errors.First();

        return error.ToProblem();
    }

    private static IResult ToProblem(this Error error)
    {
        var statusCode = error.NumericType switch
        {
            BusinessRuleError.Type => StatusCodes.Status409Conflict,
            (int)Conflict => StatusCodes.Status409Conflict,
            (int)Validation => StatusCodes.Status400BadRequest,
            (int)NotFound => StatusCodes.Status404NotFound,
            (int)Failure => StatusCodes.Status500InternalServerError,
            (int)Unexpected => StatusCodes.Status500InternalServerError,
            (int)Unauthorized => StatusCodes.Status401Unauthorized,
            (int)Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        return Results.Problem(error.Description, statusCode: statusCode);
    }
}
