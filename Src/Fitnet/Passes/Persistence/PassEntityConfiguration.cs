namespace SuperSimpleArchitecture.Fitnet.Passes.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

internal sealed class PassEntityConfiguration : IEntityTypeConfiguration<Pass>
{
    public void Configure(EntityTypeBuilder<Pass> builder)
    {
        builder.ToTable("Passes");
        builder.HasKey(pass => pass.Id);
        builder.Property(pass => pass.CustomerId).IsRequired();
        builder.Property(pass => pass.From).IsRequired();
        builder.Property(pass => pass.To).IsRequired();
        builder.Property(pass => pass.PassType).IsRequired();
    }
}