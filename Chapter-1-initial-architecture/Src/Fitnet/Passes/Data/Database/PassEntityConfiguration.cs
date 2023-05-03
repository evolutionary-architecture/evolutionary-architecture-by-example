namespace EvolutionaryArchitecture.Fitnet.Passes.Data.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class PassEntityConfiguration : IEntityTypeConfiguration<Pass>
{
    public void Configure(EntityTypeBuilder<Pass> builder)
    {
        builder.ToTable("Passes");
        builder.HasKey(pass => pass.Id);
        builder.Property(pass => pass.CustomerId).IsRequired();
        builder.Property(pass => pass.From).IsRequired();
        builder.Property(pass => pass.To).IsRequired();
    }
}