namespace EvolutionaryArchitecture.Fitnet.Contracts.Api;

using System.Reflection;
using Common.Api.Validation.Requests;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Modules;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sign;

public static class ContractsModule
{
    private static Assembly CurrentModule => typeof(SignContractRequest).Assembly;
    public static void RegisterContracts(this WebApplication app, string module)
    {
        if (!app.IsModuleEnabled(module))
        {
            return;
        }

        app.UseContracts();
        app.MapContracts();
    }

    public static IServiceCollection AddContracts(this IServiceCollection services, IConfiguration configuration,
        string module)
    {
        if (!services.IsModuleEnabled(module))
        {
            return services;
        }

        services.AddRequestsValidations(CurrentModule);
        services.AddInfrastructure(configuration);

        return services;
    }

    private static IApplicationBuilder UseContracts(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseInfrastructure();

        return applicationBuilder;
    }
}
