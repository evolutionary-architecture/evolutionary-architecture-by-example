namespace EvolutionaryArchitecture.Fitnet.Reports;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using DataAccess;
using GenerateNewPassesRegistrationsPerMonthReport;

public static class ReportsModule
{
    public static IServiceCollection AddReports(this IServiceCollection services)
    {
        services.AddDataAccess();
        services.AddNewPassesRegistrationsPerMonthReport();

        return services;
    } 

    public static IApplicationBuilder UseReports(this IApplicationBuilder applicationBuilder) => 
        applicationBuilder;
}