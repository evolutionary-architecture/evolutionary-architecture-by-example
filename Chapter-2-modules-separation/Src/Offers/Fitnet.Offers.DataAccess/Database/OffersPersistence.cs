namespace EvolutionaryArchitecture.Fitnet.Offers.DataAccess.Database;

using Microsoft.EntityFrameworkCore;

public sealed class OffersPersistence(DbContextOptions<OffersPersistence> options) : DbContext(options)
{
    private const string Schema = "Offers";

    public DbSet<Offer> Offers => Set<Offer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new OfferEntityConfiguration());
    }
}
