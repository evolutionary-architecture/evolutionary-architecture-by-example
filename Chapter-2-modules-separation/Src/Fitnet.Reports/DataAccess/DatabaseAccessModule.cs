namespace EvolutionaryArchitecture.Fitnet.Reports.DataAccess;

using Microsoft.Extensions.DependencyInjection;

internal static class DatabaseAccessModule
{
    internal static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();

        return services;
    }
}