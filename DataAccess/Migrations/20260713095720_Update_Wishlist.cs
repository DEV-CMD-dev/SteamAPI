using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update_Wishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => new { x.UserId, x.GameId });
                    table.ForeignKey(
                        name: "FK_Wishlists_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wishlists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });


            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_GameId",
                table: "Wishlists",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.CreateIndex(
                name: "IX_GameUser_WishlistId",
                table: "GameUser",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_GameUser1_UserCartId",
                table: "GameUser1",
                column: "UserCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameUser_Games_WishlistId",
                table: "GameUser",
                column: "WishlistId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameUser_Users_UserWishlistId",
                table: "GameUser",
                column: "UserWishlistId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
