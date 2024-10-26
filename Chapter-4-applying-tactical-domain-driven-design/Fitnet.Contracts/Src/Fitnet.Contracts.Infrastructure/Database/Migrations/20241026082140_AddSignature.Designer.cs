﻿// <auto-generated />
using System;
using EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Migrations
{
    [DbContext(typeof(ContractsPersistence))]
    [Migration("20241026082140_AddSignature")]
    partial class AddSignature
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Contracts")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EvolutionaryArchitecture.Fitnet.Contracts.Core.BindingContract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("BindingFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<DateTimeOffset>("ExpiringAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("TerminatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("BindingContracts", "Contracts");
                });

            modelBuilder.Entity("EvolutionaryArchitecture.Fitnet.Contracts.Core.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<DateTimeOffset?>("ExpiringAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("PreparedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Contracts", "Contracts");
                });

            modelBuilder.Entity("EvolutionaryArchitecture.Fitnet.Contracts.Core.BindingContract", b =>
                {
                    b.OwnsMany("EvolutionaryArchitecture.Fitnet.Contracts.Core.Annex", "AttachedAnnexes", b1 =>
                        {
                            b1.Property<Guid>("BindingContractId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<DateTimeOffset>("ValidFrom")
                                .HasColumnType("timestamp with time zone");

                            b1.HasKey("BindingContractId", "Id");

                            b1.ToTable("Annexes", "Contracts");

                            b1.WithOwner()
                                .HasForeignKey("BindingContractId");
                        });

                    b.Navigation("AttachedAnnexes");
                });

            modelBuilder.Entity("EvolutionaryArchitecture.Fitnet.Contracts.Core.Contract", b =>
                {
                    b.OwnsOne("EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract.Signatures.DigitalSignature", "Signature", b1 =>
                        {
                            b1.Property<Guid>("ContractId")
                                .HasColumnType("uuid");

                            b1.Property<DateTimeOffset>("Date")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<string>("Signature")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.HasKey("ContractId");

                            b1.ToTable("Contracts", "Contracts");

                            b1.WithOwner()
                                .HasForeignKey("ContractId");
                        });

                    b.Navigation("Signature");
                });
#pragma warning restore 612, 618
        }
    }
}