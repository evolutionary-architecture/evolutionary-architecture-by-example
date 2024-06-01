namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Common.Errors;

using Fitnet.Common.Core.BussinessRules;
using Microsoft.AspNetCore.Http;
using static ErrorType;

internal static class ProblemResults
{
    internal static IResult ToProblem(this IReadOnlyCollection<Error> errors)
    {
        var error = errors.First();
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

        var errorMessage = GetErrorMessage(errors);
        var results = Results.Problem(errorMessage, statusCode: statusCode);

        return results;
    }

    private const string? Separator = ", ";
    private static string GetErrorMessage(IEnumerable<Error> errors) =>
        string.Join(Separator, errors.Select(error => error.Description));
}
