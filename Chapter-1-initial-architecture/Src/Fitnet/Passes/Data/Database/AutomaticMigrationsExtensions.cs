namespace EvolutionaryArchitecture.Fitnet.Passes.Data.Database;

internal static class AutomaticMigrationsExtensions
{
    internal static IApplicationBuilder UseAutomaticMigrations(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PassesPersistence>();
        context.Database.EnsureCreated();

        return applicationBuilder;
    }
}
