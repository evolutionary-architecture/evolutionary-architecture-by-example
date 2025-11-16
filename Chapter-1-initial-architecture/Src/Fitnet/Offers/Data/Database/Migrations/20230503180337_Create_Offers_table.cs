#nullable disable

namespace SuperSimpleArchitecture.Fitnet.Migrations.OffersPersistenceMigrations;

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

[ExcludeFromCodeCoverage]
public partial class CreateOfferTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Offers");

        migrationBuilder.CreateTable(
            name: "Offers",
            schema: "Offers",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                PreparedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                Discount = table.Column<decimal>(type: "numeric", nullable: false),
                OfferedFromDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                OfferedFromTo = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Offers", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
            name: "Offers",
            schema: "Offers");
}
