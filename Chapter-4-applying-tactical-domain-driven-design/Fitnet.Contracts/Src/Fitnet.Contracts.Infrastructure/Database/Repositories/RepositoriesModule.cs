namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Repositories;

using Application;
using Microsoft.Extensions.DependencyInjection;

internal static class RepositoriesModule
{
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IContractsRepository, ContractsRepository>();
        services.AddScoped<IBindingContractsRepository, BindingContractsRepository>();

        return services;
    }
}
