using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperSimpleArchitecture.Fitnet.Migrations.ContractsPersistenceMigrations
{
    /// <inheritdoc />
    public partial class AddPreparedAtDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PreparedAt",
                schema: "Contracts",
                table: "Contracts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: DateTimeOffset.Now);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreparedAt",
                schema: "Contracts",
                table: "Contracts");
        }
    }
}
