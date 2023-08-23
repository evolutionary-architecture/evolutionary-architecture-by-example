namespace EvolutionaryArchitecture.Fitnet.Common.Api.ErrorHandling;

using Microsoft.AspNetCore.Builder;

public static class ErrorHandlingExtensions
{
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<ExceptionMiddleware>();

        return applicationBuilder;
    }
}