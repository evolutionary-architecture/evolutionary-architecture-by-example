namespace EvolutionaryArchitecture.Fitnet.Offers.Data.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class OfferEntityConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("Offers");
        builder.HasKey(offers => offers.Id);
        builder.Property(offers => offers.PreparedAt).IsRequired();
        builder.Property(offers => offers.OfferedFromTo).IsRequired();
        builder.Property(offers => offers.OfferedFromDate).IsRequired();
        builder.Property(offers => offers.Discount).IsRequired();
        builder.Property(offers => offers.CustomerId).IsRequired();
    }
}