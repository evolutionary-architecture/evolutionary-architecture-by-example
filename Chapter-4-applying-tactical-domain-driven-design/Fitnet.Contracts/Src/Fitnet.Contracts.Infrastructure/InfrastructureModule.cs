namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure;

using Application;
using Database;
using EventBus;
using Mediation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddMediationModule();
        services.AddEventBus(configuration);
        services.AddScoped<IContractsModule, ContractsModule>();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();

        return applicationBuilder;
    }
}
