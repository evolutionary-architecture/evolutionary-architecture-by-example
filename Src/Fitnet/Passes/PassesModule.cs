namespace SuperSimpleArchitecture.Fitnet.Passes;

using Persistence;

internal static class PassesModule
{
    internal static IServiceCollection AddPasses(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistenceModule(configuration);

        return services;
    }
    
    internal static IApplicationBuilder UsePasses(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UsePersistenceModule();

        return applicationBuilder;
    }
}