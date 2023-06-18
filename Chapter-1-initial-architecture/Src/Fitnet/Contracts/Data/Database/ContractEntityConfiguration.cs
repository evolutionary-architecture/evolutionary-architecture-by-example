namespace EvolutionaryArchitecture.Fitnet.Contracts.Data.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class ContractEntityConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("Contracts");
        builder.HasKey(contract => contract.Id);
        builder.Property(contract => contract.PreparedAt).IsRequired();
        builder.Property(contract => contract.Duration).IsRequired();
        builder.Property(contract => contract.ExpiringAt).IsRequired(false);
    }
}