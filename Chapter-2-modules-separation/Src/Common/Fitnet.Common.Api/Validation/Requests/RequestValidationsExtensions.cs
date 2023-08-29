namespace EvolutionaryArchitecture.Fitnet.Common.Api.Validation.Requests;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class RequestValidationsExtensions
{
    public static IServiceCollection AddRequestsValidations<T>(this IServiceCollection services) => 
        services.AddValidatorsFromAssemblyContaining<T>(includeInternalTypes: true);
}