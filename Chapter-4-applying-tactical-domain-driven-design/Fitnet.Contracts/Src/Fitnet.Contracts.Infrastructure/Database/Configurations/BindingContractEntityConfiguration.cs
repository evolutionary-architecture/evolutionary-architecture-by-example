﻿namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Configurations;

using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class BindingContractEntityConfiguration : IEntityTypeConfiguration<BindingContract>
{
    public void Configure(EntityTypeBuilder<BindingContract> builder)
    {
        builder.ToTable("BindingContracts");
        builder.HasKey(contract => contract.Id);
        builder
            .Property(contract => contract.Id)
            .HasConversion(
                id => id.Value,
                value => new BindingContractId(value))
            .ValueGeneratedOnAdd();
        builder
            .Property(contract => contract.ContractId)
            .HasConversion(
                id => id.Value,
                value => new ContractId(value));
        builder.Property(contract => contract.ContractId).IsRequired();
        builder.Property(contract => contract.CustomerId).IsRequired();
        builder.Property(contract => contract.Duration).IsRequired();
        builder.Property(contract => contract.TerminatedAt).IsRequired();
        builder.Property(contract => contract.BindingFrom).IsRequired();
        builder.Property(contract => contract.ExpiringAt).IsRequired();

        ConfigureAttachedAnnexes(builder);
    }

    private static void ConfigureAttachedAnnexes(EntityTypeBuilder<BindingContract> builder) =>
        builder.OwnsMany<Annex>(nameof(BindingContract.AttachedAnnexes), annex =>
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
