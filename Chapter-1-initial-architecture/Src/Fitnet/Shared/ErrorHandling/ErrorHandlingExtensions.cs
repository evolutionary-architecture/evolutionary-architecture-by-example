namespace EvolutionaryArchitecture.Fitnet.Shared.ErrorHandling;

internal static class ErrorHandlingExtensions
{
    internal static IApplicationBuilder UseErrorHandling(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<ExceptionMiddleware>();

        return applicationBuilder;
    }
}