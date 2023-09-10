namespace EvolutionaryArchitecture.Fitnet.Contracts.Api;

using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ContractsModule
{
    public static void RegisterContractsApi(this WebApplication app)
    {
        app.UseContracts();
        app.MapContracts();
    }

    public static IServiceCollection AddContractsApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        return services;
    }

    private static IApplicationBuilder UseContracts(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseInfrastructure();

        return applicationBuilder;
    }
}
