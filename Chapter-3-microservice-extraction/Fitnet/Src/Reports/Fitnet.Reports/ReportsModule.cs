namespace EvolutionaryArchitecture.Fitnet.Reports;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using DataAccess;
using GenerateNewPassesRegistrationsPerMonthReport;
using Microsoft.Extensions.Configuration;
using Common.Infrastructure.Modules;

public static class ReportsModule
{
    public static void RegisterReports(this WebApplication app, string module)
    {
        if (!app.Configuration.IsModuleEnabled(module))
        {
            return;
        }

        app.UseReports();
        app.MapReports();
    }

    public static IServiceCollection AddReports(this IServiceCollection services, IConfiguration configuration, string module)
    {
        if (!configuration.IsModuleEnabled(module))
        {
            return services;
        }

        services.AddDataAccess();
        services.AddNewPassesRegistrationsPerMonthReport();

        return services;
    }

    private static IApplicationBuilder UseReports(this IApplicationBuilder applicationBuilder) =>
        applicationBuilder;
}
