namespace EvolutionaryArchitecture.Fitnet.Reports;

using Common.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using DataAccess;
using GenerateNewPassesRegistrationsPerMonthReport;

public static class ReportsModule
{
    public static void RegisterReports(this WebApplication app, string module)
    {
        if (!app.IsModuleEnabled(module))
        {
            return;
        }

        app.UseReports();
        app.MapReports();
    }

    public static IServiceCollection AddReports(this IServiceCollection services, string module)
    {
        if (!services.IsModuleEnabled(module))
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
