namespace EvolutionaryArchitecture.Fitnet.Common.Validation.Requests;

using FluentValidation;
using Logging;

internal static class RequestValidationsExtensions
{
    internal static IServiceCollection AddRequestsValidations(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(Program))
            .AddClasses(classes => classes.AssignableTo<IValidator>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        services.AddSingleton<IRequestValidationLogger, RequestValidationLogger>();

        return services;
    }
}