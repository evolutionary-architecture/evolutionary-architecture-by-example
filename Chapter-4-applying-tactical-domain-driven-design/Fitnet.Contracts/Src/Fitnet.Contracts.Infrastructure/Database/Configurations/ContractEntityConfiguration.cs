namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Configurations;

using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class ContractEntityConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("Contracts");
        builder.HasKey(contract => contract.Id);
        builder
            .Property(contract => contract.Id)
            .HasConversion(
                id => id.Value,
                value => new ContractId(value))
            .ValueGeneratedOnAdd();
        builder.Property(contract => contract.PreparedAt).IsRequired();
        builder.Property(contract => contract.SignedAt).IsRequired(false);
    }
}
