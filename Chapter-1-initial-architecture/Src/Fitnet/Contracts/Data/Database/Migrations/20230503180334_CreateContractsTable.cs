#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Contracts.Data.Database.Migrations;

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

[ExcludeFromCodeCoverage]
public partial class CreateContractsTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Contracts");

        migrationBuilder.CreateTable(
            name: "Contracts",
            schema: "Contracts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Contracts", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
            name: "Contracts",
            schema: "Contracts");
}
