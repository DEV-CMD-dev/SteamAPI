using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddWalletBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Games_Users_DeveloperId",
            //    table: "Games",
            //    column: "DeveloperId",
            //    principalTable: "Users",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_GameTag_Games_GamesId",
            //    table: "GameTag",
            //    column: "GamesId",
            //    principalTable: "Games",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_GameTag_Tags_TagsId",
            //    table: "GameTag",
            //    column: "TagsId",
            //    principalTable: "Tags",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_GameVersions_Games_GameId",
            //    table: "GameVersions",
            //    column: "GameId",
            //    principalTable: "Games",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_InventoryItems_Items_ItemId",
            //    table: "InventoryItems",
            //    column: "ItemId",
            //    principalTable: "Items",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_InventoryItems_Users_UserId",
            //    table: "InventoryItems",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Items_Games_GameId",
            //    table: "Items",
            //    column: "GameId",
            //    principalTable: "Games",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Profiles_Users_UserId",
            //    table: "Profiles",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_RoleClaims_Roles_RoleId",
            //    table: "RoleClaims",
            //    column: "RoleId",
            //    principalTable: "Roles",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Screenshots_Games_GameId",
            //    table: "Screenshots",
            //    column: "GameId",
            //    principalTable: "Games",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TradeOffers_Users_ReceiverId",
            //    table: "TradeOffers",
            //    column: "ReceiverId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TradeOffers_Users_SenderId",
            //    table: "TradeOffers",
            //    column: "SenderId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserAchievements_Achievements_AchievementId",
            //    table: "UserAchievements",
            //    column: "AchievementId",
            //    principalTable: "Achievements",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserAchievements_Users_UserId",
            //    table: "UserAchievements",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserClaims_Users_UserId",
            //    table: "UserClaims",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserGames_Games_GameId",
            //    table: "UserGames",
            //    column: "GameId",
            //    principalTable: "Games",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserGames_Users_UserId",
            //    table: "UserGames",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserLogins_Users_UserId",
            //    table: "UserLogins",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserRoles_Roles_RoleId",
            //    table: "UserRoles",
            //    column: "RoleId",
            //    principalTable: "Roles",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserRoles_Users_UserId",
            //    table: "UserRoles",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserTokens_Users_UserId",
            //    table: "UserTokens",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Wishlists_Games_GameId",
            //    table: "Wishlists",
            //    column: "GameId",
            //    principalTable: "Games",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Wishlists_Users_UserId",
            //    table: "Wishlists",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_DeveloperId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTag_Games_GamesId",
                table: "GameTag");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTag_Tags_TagsId",
                table: "GameTag");

            migrationBuilder.DropForeignKey(
                name: "FK_GameVersions_Games_GameId",
                table: "GameVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_Items_ItemId",
                table: "InventoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_Users_UserId",
                table: "InventoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Games_GameId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Users_UserId",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaims_Roles_RoleId",
                table: "RoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_Screenshots_Games_GameId",
                table: "Screenshots");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeOffers_Users_ReceiverId",
                table: "TradeOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeOffers_Users_SenderId",
                table: "TradeOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Achievements_AchievementId",
                table: "UserAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Users_UserId",
                table: "UserAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_Users_UserId",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_Games_GameId",
                table: "UserGames");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_Users_UserId",
                table: "UserGames");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Users_UserId",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Users_UserId",
                table: "UserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Games_GameId",
                table: "Wishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Users_UserId",
                table: "Wishlists");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
