namespace EvolutionaryArchitecture.Fitnet.Common.Api.ErrorHandling;

using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

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
            case ResourceNotFoundException resourceNotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                message = resourceNotFoundException.Message;
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

public record ExceptionResponseMessage(int StatusCode, string Message);
