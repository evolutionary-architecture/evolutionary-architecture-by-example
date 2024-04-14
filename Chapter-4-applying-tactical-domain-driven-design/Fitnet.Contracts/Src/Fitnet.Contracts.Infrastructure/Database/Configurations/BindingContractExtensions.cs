namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Configurations;

using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal static class BindingContractExtensions
{
    internal static void RegisterAnnexes(this EntityTypeBuilder<BindingContract> builder) => builder.OwnsMany<Annex>(
        nameof(BindingContract.AttachedAnnexes), annex =>
        {
            annex.WithOwner().HasForeignKey(a => a.BindingContractId);
            annex.ToTable("Annexes");
            annex
                .Property(annex => annex.Id)
                .HasConversion(
                    id => id.Value,
                    value => new AnnexId(value));
            annex.Property(annex => annex.Id).IsRequired();
            annex.Property(annex => annex.BindingContractId)
                .HasConversion(
                    id => id.Value,
                    value => new BindingContractId(value));
            annex.Property(annex => annex.BindingContractId).IsRequired();
            annex.Property(annex => annex.ValidFrom).IsRequired();
        });
}
