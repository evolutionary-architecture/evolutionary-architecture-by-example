namespace EvolutionaryArchitecture.Fitnet.Passes.DataAccess.Database;

using MassTransit;
using Microsoft.EntityFrameworkCore;

public sealed class PassesPersistence(DbContextOptions<PassesPersistence> options) : DbContext(options)
{
    private const string Schema = "Passes";

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
