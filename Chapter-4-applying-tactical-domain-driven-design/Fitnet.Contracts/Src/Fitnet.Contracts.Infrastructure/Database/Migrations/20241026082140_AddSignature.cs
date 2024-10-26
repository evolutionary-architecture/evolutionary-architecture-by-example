#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;

/// <inheritdoc />
public partial class AddSignature : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "SignedAt",
            schema: "Contracts",
            table: "Contracts",
            newName: "Signature_Date");

        migrationBuilder.AddColumn<string>(
            name: "Signature_Signature",
            schema: "Contracts",
            table: "Contracts",
            type: "character varying(100)",
            maxLength: 100,
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Signature_Signature",
            schema: "Contracts",
            table: "Contracts");

        migrationBuilder.RenameColumn(
            name: "Signature_Date",
            schema: "Contracts",
            table: "Contracts",
            newName: "SignedAt");
    }
}
