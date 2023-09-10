namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database;

using Core;
using Microsoft.EntityFrameworkCore;

public sealed class ContractsPersistence : DbContext
{
    private const string Schema = "Contracts";

    public ContractsPersistence(DbContextOptions<ContractsPersistence> options)
        : base(options)
    {
    }

    public DbSet<Contract> Contracts => Set<Contract>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new ContractEntityConfiguration());
    }
}
