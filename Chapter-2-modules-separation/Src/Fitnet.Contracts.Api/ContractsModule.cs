namespace EvolutionaryArchitecture.Fitnet.Contracts.Api;

using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ContractsModule
{
    public static IServiceCollection AddContracts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        return services;
    }
    
    public static IApplicationBuilder UseContracts(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseInfrastructure();

        return applicationBuilder;
    }
}