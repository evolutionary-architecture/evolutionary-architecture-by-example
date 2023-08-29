namespace EvolutionaryArchitecture.Fitnet.Contracts.Api;

using Common.Api.Validation.Requests;
using EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Modules;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ContractsModule
{
    public static void RegisterContracts(this WebApplication app, string module)
    {
        if (!app.IsModuleEnabled(module)) return;

        app.UseContracts();
        app.MapContracts();
    }

    public static IServiceCollection AddContracts(this IServiceCollection services, IConfiguration configuration,
        string module)
    {
        if (!services.IsModuleEnabled(module)) return services;

        services.AddRequestsValidations<CurrentAssembly>();
        services.AddInfrastructure(configuration);

        return services;
    }

    private static IApplicationBuilder UseContracts(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseInfrastructure();

        return applicationBuilder;
    }
}