namespace EvolutionaryArchitecture.Fitnet.Common.Validation.Requests;

using FluentValidation;
using Logging;

internal static class RequestValidationsExtensions
{
    internal static IServiceCollection AddRequestsValidations(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Program>(includeInternalTypes: true);
        services.AddSingleton<IRequestValidationLogger, RequestValidationLogger>();

        return services;
    }
}