#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Contracts.Data.Database.Migrations;

using System;
using Microsoft.EntityFrameworkCore.Migrations;

[ExcludeFromCodeCoverage]
public partial class ContractsAddColumnsToSupportContractExpiration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<TimeSpan>(
            name: "Duration",
            schema: "Contracts",
            table: "Contracts",
            type: "interval",
            nullable: false,
            defaultValue: new TimeSpan(0, 0, 0, 0, 0));

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "ExpiringAt",
            schema: "Contracts",
            table: "Contracts",
            type: "timestamp with time zone",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Duration",
            schema: "Contracts",
            table: "Contracts");

        migrationBuilder.DropColumn(
            name: "ExpiringAt",
            schema: "Contracts",
            table: "Contracts");
    }
}
