#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database.Migrations;
using System;
using Microsoft.EntityFrameworkCore.Migrations;

/// <inheritdoc />
public partial class ChangeBindingContractTerminatedAtToNullable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.AlterColumn<DateTimeOffset>(
            name: "TerminatedAt",
            schema: "Contracts",
            table: "BindingContracts",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateTimeOffset),
            oldType: "timestamp with time zone");

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.AlterColumn<DateTimeOffset>(
            name: "TerminatedAt",
            schema: "Contracts",
            table: "BindingContracts",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
            oldClrType: typeof(DateTimeOffset),
            oldType: "timestamp with time zone",
            oldNullable: true);
}
