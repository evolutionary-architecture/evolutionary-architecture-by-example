namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Configurations;

using Core;
using Core.SignContract.Signatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
            signatureBuilder.Property(signature => signature.Text).IsRequired().HasMaxLength(100);
        });
    }
}

public class BloggingContextFactory : IDesignTimeDbContextFactory<ContractsPersistence>
{
    public ContractsPersistence CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ContractsPersistence>();
        optionsBuilder.UseNpgsql("Host=localhost:5432;Database=fitnet;Username=postgres;Password=mysecretpassword");

        return new ContractsPersistence(optionsBuilder.Options);
    }
}
