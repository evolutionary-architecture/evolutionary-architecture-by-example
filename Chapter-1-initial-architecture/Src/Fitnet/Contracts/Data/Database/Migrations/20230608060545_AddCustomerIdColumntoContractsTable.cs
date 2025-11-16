#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Contracts.Data.Database.Migrations;

using System;
using Microsoft.EntityFrameworkCore.Migrations;

[ExcludeFromCodeCoverage]
public partial class AddCustomerIdColumntoContractsTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.AddColumn<Guid>(
            name: "CustomerId",
            schema: "Contracts",
            table: "Contracts",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropColumn(
            name: "CustomerId",
            schema: "Contracts",
            table: "Contracts");
}
