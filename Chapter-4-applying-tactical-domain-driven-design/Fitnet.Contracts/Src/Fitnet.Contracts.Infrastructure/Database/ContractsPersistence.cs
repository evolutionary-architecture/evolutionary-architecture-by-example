namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database;

using Configurations;
using Core;
using Microsoft.EntityFrameworkCore;

public sealed class ContractsPersistence(DbContextOptions<ContractsPersistence> options) : DbContext(options)
{
    private const string Schema = "Contracts";

    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<BindingContract> BindingContracts => Set<BindingContract>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new ContractEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BindingContractEntityConfiguration());
    }
}
