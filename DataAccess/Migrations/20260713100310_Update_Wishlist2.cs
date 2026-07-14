using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update_Wishlist2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "GameUser");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => new { x.UserId, x.GameId });
                    table.ForeignKey(
                        name: "FK_Carts_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_GameId",
                table: "Carts",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            //migrationBuilder.CreateTable(
            //    name: "GameUser",
            //    columns: table => new
            //    {
            //        CartId = table.Column<int>(type: "int", nullable: false),
            //        UserCartId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_GameUser", x => new { x.CartId, x.UserCartId });
            //        table.ForeignKey(
            //            name: "FK_GameUser_Games_CartId",
            //            column: x => x.CartId,
            //            principalTable: "Games",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_GameUser_Users_UserCartId",
            //            column: x => x.UserCartId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_GameUser_UserCartId",
            //    table: "GameUser",
            //    column: "UserCartId");
        }
    }
}
