namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Repositories;

using Core;
using Microsoft.Extensions.DependencyInjection;

internal static class RepositoriesModule
{
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IContractsRepository, ContractsRepository>();
        services.AddSingleton<IBindingContractsRepository, InMemoryBindingContractsRepository>();

        return services;
    }
}
