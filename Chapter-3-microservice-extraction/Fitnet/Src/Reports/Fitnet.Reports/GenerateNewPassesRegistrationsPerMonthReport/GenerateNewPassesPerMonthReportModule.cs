namespace EvolutionaryArchitecture.Fitnet.Reports.GenerateNewPassesRegistrationsPerMonthReport;

using Microsoft.Extensions.DependencyInjection;
using DataRetriever;

internal static class GenerateNewPassesPerMonthReportModule
{
    internal static IServiceCollection AddNewPassesRegistrationsPerMonthReport(this IServiceCollection services)
    {
        services
            .AddSingleton<INewPassesRegistrationPerMonthReportDataRetriever,
                NewPassesRegistrationPerMonthReportDataRetriever>();

        return services;
    }
}