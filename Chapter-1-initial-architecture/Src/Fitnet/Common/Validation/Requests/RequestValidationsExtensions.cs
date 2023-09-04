namespace EvolutionaryArchitecture.Fitnet.Common.Validation.Requests;

using FluentValidation;

internal static class RequestValidationsExtensions
{
    internal static IServiceCollection AddRequestsValidations(this IServiceCollection services) =>
        services.AddValidatorsFromAssemblyContaining<Program>(includeInternalTypes: true);
}
