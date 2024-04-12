#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Migrations;

using System;
using Microsoft.EntityFrameworkCore.Migrations;

/// <inheritdoc />
public partial class AddAnnexes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.CreateTable(
            name: "Annexes",
            schema: "Contracts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                BindingContractId = table.Column<Guid>(type: "uuid", nullable: false),
                ValidFrom = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Annexes", x => new { x.BindingContractId, x.Id });
                table.ForeignKey(
                    name: "FK_Annexes_BindingContracts_BindingContractId",
                    column: x => x.BindingContractId,
                    principalSchema: "Contracts",
                    principalTable: "BindingContracts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
            name: "Annexes",
            schema: "Contracts");
}
