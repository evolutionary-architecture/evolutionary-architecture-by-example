namespace EvolutionaryArchitecture.Fitnet.Offers.DataAccess.Database;

using Microsoft.EntityFrameworkCore;

public sealed class OffersPersistence : DbContext
{
    private const string Schema = "Offers";

    public OffersPersistence(DbContextOptions<OffersPersistence> options)
        : base(options)
    {
    }

    public DbSet<Offer> Offers => Set<Offer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new OfferEntityConfiguration());
    }
}