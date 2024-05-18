namespace EvolutionaryArchitecture.Fitnet.Reports;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Common.Infrastructure.Modules;
using DataAccess;
using GenerateNewPassesRegistrationsPerMonthReport;
using Microsoft.Extensions.Configuration;

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

    public static IServiceCollection AddReports(this IServiceCollection services, string module, IConfiguration configuration)
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
