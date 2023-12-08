namespace EvolutionaryArchitecture.Fitnet.Common.ErrorHandling;

internal static class ErrorHandlingExtensions
{
    internal static IApplicationBuilder UseErrorHandling(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseExceptionHandler();

        return applicationBuilder;
    }

    internal static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}
