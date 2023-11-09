namespace EvolutionaryArchitecture.Fitnet.Passes.DataAccess.Database;

using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public sealed class PassesPersistence : DbContext
{
    private const string Schema = "Passes";

    public PassesPersistence(DbContextOptions<PassesPersistence> options)
        : base(options)
    {
    }

    public DbSet<Pass> Passes => Set<Pass>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new PassEntityConfiguration());
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}

public class BloggingContextFactory : IDesignTimeDbContextFactory<PassesPersistence>
{

    public PassesPersistence CreateDbContext(string[] args)

    {

        var optionsBuilder = new DbContextOptionsBuilder<PassesPersistence>();

        optionsBuilder.UseNpgsql("Host=localhost:5432;Database=sim_inventory;Username=postgres;Password=mysecretpassword");

        return new PassesPersistence(optionsBuilder.Options);

    }

}
