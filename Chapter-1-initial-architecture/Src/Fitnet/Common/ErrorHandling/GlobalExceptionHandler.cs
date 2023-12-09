namespace EvolutionaryArchitecture.Fitnet.Common.ErrorHandling;

using BusinessRulesEngine;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private const string? ServerError = "Server Error";
    private const string ErrorOccurredMessage = "An error occurred.";

    private static readonly Action<ILogger, string, Exception> LogException =
        LoggerMessage.Define<string>(LogLevel.Error, eventId:
            new EventId(0, "ERROR"), formatString: "{Message}");

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        LogException(logger, ErrorOccurredMessage, exception);
        var problemDetails = exception switch
        {
            BusinessRuleValidationException businessRuleValidationException => new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = businessRuleValidationException.Message
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = ServerError
            },
        };

        httpContext.Response.StatusCode = problemDetails.Status!.Value;
        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
