using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEnitityUserGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameUser",
                columns: table => new
                {
                    UserWishlistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WishlistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUser", x => new { x.UserWishlistId, x.WishlistId });
                    table.ForeignKey(
                        name: "FK_GameUser_Games_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameUser_Users_UserWishlistId",
                        column: x => x.UserWishlistId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameUser1",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false),
                    UserCartId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUser1", x => new { x.CartId, x.UserCartId });
                    table.ForeignKey(
                        name: "FK_GameUser1_Games_CartId",
                        column: x => x.CartId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameUser1_Users_UserCartId",
                        column: x => x.UserCartId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameUser_WishlistId",
                table: "GameUser",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_GameUser1_UserCartId",
                table: "GameUser1",
                column: "UserCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameUser");

            migrationBuilder.DropTable(
                name: "GameUser1");
        }
    }
}