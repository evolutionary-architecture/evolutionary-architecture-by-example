#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Migrations;

using System;
using Microsoft.EntityFrameworkCore.Migrations;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public partial class CreatePassesTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Passes");

        migrationBuilder.CreateTable(
            name: "Passes",
            schema: "Passes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                From = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                To = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Passes", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
            name: "Passes",
            schema: "Passes");
}
