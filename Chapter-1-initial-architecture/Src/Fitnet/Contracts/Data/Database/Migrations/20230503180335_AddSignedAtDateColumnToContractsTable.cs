#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Contracts.Data.Database.Migrations;

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

[ExcludeFromCodeCoverage]
public partial class AddSignedAtDateColumnToContracts : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.AddColumn<DateTimeOffset>(
            name: "SignedAt",
            schema: "Contracts",
            table: "Contracts",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: DateTimeOffset.Now);

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropColumn(
            name: "SignedAt",
            schema: "Contracts",
            table: "Contracts");
}
