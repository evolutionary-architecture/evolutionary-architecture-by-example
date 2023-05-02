namespace SuperSimpleArchitecture.Fitnet.Offers.Data.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class OfferEntityConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("Offers");
        builder.HasKey(offers => offers.Id);
        builder.Property(offers => offers.PreparedAt).IsRequired();
    }
}