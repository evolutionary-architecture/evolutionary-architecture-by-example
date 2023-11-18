namespace EvolutionaryArchitecture.Fitnet.Common.ErrorHandling;

using System.Net;
using System.Text.Json;
using BusinessRulesEngine;

internal sealed class ExceptionMiddleware(RequestDelegate next)
{
    private const string ContentType = "application/json";

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = ContentType;

        int statusCode;
        string message;

        switch (exception)
        {
            case BusinessRuleValidationException businessRuleValidationException:
                statusCode = (int)HttpStatusCode.Conflict;
                message = businessRuleValidationException.Message;
                break;
            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = exception.Message;
                break;
        }

        context.Response.StatusCode = statusCode;

        var result = JsonSerializer.Serialize(new ExceptionResponseMessage(statusCode, message));

        await context.Response.WriteAsync(result);
    }
}

internal record ExceptionResponseMessage(int StatusCode, string Message);
