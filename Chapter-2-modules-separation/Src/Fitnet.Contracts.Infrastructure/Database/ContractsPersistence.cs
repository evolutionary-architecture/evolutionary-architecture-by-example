namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database;

using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class BloggingContextFactory : IDesignTimeDbContextFactory<ContractsPersistence>
{
    public ContractsPersistence CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ContractsPersistence>();
        optionsBuilder.UseNpgsql("Host=localhost:5432;Database=fitnet;Username=postgres;Password=mysecretpassword");

        return new ContractsPersistence(optionsBuilder.Options);
    }
}

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