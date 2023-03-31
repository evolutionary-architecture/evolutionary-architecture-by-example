namespace SuperSimpleArchitecture.Fitnet.Reports;

internal static class ReportsModule
{
    internal static IServiceCollection AddReports(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
    
    internal static IApplicationBuilder UseReports(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder;
    }
}