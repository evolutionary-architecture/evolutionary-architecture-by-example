namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Configurations;

using Core;
using Core.SignContract.Signatures;
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
        builder.OwnsOne<Signature>("Signature", signatureBuilder =>
        {
            signatureBuilder.Property(signature => signature.Date).IsRequired();
            signatureBuilder.Property(signature => signature.Value).IsRequired().HasMaxLength(100);
        });
    }
}
