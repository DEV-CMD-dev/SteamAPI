using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGameIconUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "IconUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "IconUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "IconUrl",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_TradeOffers_ReceiverInventoryItemId",
                table: "TradeOffers",
                column: "ReceiverInventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeOffers_SenderInventoryItemId",
                table: "TradeOffers",
                column: "SenderInventoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_TradeOffers_InventoryItems_ReceiverInventoryItemId",
                table: "TradeOffers",
                column: "ReceiverInventoryItemId",
                principalTable: "InventoryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TradeOffers_InventoryItems_SenderInventoryItemId",
                table: "TradeOffers",
                column: "SenderInventoryItemId",
                principalTable: "InventoryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradeOffers_InventoryItems_ReceiverInventoryItemId",
                table: "TradeOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeOffers_InventoryItems_SenderInventoryItemId",
                table: "TradeOffers");

            migrationBuilder.DropIndex(
                name: "IX_TradeOffers_ReceiverInventoryItemId",
                table: "TradeOffers");

            migrationBuilder.DropIndex(
                name: "IX_TradeOffers_SenderInventoryItemId",
                table: "TradeOffers");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Games");
        }
    }
}
