#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Contracts.Data.Database.Migrations;
using System;
using Microsoft.EntityFrameworkCore.Migrations;

/// <inheritdoc />
public partial class AddBindingContractTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.CreateTable(
            name: "BindingContracts",
            schema: "Contracts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                ContractId = table.Column<Guid>(type: "uuid", nullable: false),
                CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                TerminatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                BindingFrom = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                ExpiringAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BindingContracts", x => x.Id);
            });

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
            name: "BindingContracts",
            schema: "Contracts");
}
