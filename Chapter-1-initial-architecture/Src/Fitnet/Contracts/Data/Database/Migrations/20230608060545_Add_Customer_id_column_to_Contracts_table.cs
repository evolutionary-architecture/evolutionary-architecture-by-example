using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Contracts.Data.Database.Migrations;

[ExcludeFromCodeCoverage]
public partial class AddCustomeridcolumntoContractstable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "CustomerId",
            schema: "Contracts",
            table: "Contracts",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CustomerId",
            schema: "Contracts",
            table: "Contracts");
    }
}