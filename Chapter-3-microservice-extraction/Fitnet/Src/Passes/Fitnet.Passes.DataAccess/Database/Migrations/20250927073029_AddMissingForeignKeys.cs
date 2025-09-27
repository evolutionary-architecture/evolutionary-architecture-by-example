#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Passes.DataAccess.Database.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;

/// <inheritdoc />
public partial class AddMissingForeignKeys : Migration
{
    private static readonly string[] columns = ["InboxMessageId", "InboxConsumerId"];
    private static readonly string[] principalColumns = ["MessageId", "ConsumerId"];

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddForeignKey(
            name: "FK_OutboxMessage_InboxState_InboxMessageId_InboxConsumerId",
            schema: "Passes",
            table: "OutboxMessage",
            columns: columns,
            principalSchema: "Passes",
            principalTable: "InboxState",
            principalColumns: principalColumns);

        migrationBuilder.AddForeignKey(
            name: "FK_OutboxMessage_OutboxState_OutboxId",
            schema: "Passes",
            table: "OutboxMessage",
            column: "OutboxId",
            principalSchema: "Passes",
            principalTable: "OutboxState",
            principalColumn: "OutboxId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_OutboxMessage_InboxState_InboxMessageId_InboxConsumerId",
            schema: "Passes",
            table: "OutboxMessage");

        migrationBuilder.DropForeignKey(
            name: "FK_OutboxMessage_OutboxState_OutboxId",
            schema: "Passes",
            table: "OutboxMessage");
    }
}
