namespace EvolutionaryArchitecture.Fitnet.Passes.Api;

using Contracts.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class PassesModule
{
    public static IServiceCollection AddPasses(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccess(configuration);
        
        return services;
    }
    
    public static IApplicationBuilder UsePasses(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDataAccess();
        
        return applicationBuilder;
    }
}