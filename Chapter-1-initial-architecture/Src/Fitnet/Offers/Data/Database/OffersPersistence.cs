namespace EvolutionaryArchitecture.Fitnet.Offers.Data.Database;

using Microsoft.EntityFrameworkCore;

internal sealed class OffersPersistence(DbContextOptions<OffersPersistence> options) : DbContext(options)
{
    private const string Schema = "Offers";

    public DbSet<Offer> Offers => Set<Offer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new OfferEntityConfiguration());
    }
}
