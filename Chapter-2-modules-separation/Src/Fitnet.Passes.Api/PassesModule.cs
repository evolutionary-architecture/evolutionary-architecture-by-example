namespace EvolutionaryArchitecture.Fitnet.Passes.Api;

using DataAccess;
using System.Reflection;
using Common.Infrastructure.Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class PassesModule
{
    public static IServiceCollection AddPasses(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccess(configuration);
        services.AddMediator(Assembly.GetExecutingAssembly());
        
        return services;
    }
    
    public static IApplicationBuilder UsePasses(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDataAccess();
        
        return applicationBuilder;
    }
}