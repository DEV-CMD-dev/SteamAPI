using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class masd2134 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTag_Tag_TagsId",
                table: "GameTag");

            migrationBuilder.DropForeignKey(
                name: "FK_GameVersion_Game_GameId",
                table: "GameVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_OwnedGame_Game_GameId",
                table: "OwnedGame");

            migrationBuilder.DropForeignKey(
                name: "FK_OwnedGame_Users_UserId",
                table: "OwnedGame");

            migrationBuilder.DropForeignKey(
                name: "FK_Screenshot_Game_GameId",
                table: "Screenshot");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievement_Achievement_AchievementId",
                table: "UserAchievement");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievement_Users_UserId",
                table: "UserAchievement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAchievement",
                table: "UserAchievement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Screenshot",
                table: "Screenshot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnedGame",
                table: "OwnedGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameVersion",
                table: "GameVersion");

            migrationBuilder.RenameTable(
                name: "UserAchievement",
                newName: "UserAchievements");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Screenshot",
                newName: "Screenshots");

            migrationBuilder.RenameTable(
                name: "OwnedGame",
                newName: "OwnedGames");

            migrationBuilder.RenameTable(
                name: "GameVersion",
                newName: "GameVersions");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchievement_UserId",
                table: "UserAchievements",
                newName: "IX_UserAchievements_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchievement_AchievementId",
                table: "UserAchievements",
                newName: "IX_UserAchievements_AchievementId");

            migrationBuilder.RenameIndex(
                name: "IX_Screenshot_GameId",
                table: "Screenshots",
                newName: "IX_Screenshots_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedGame_UserId",
                table: "OwnedGames",
                newName: "IX_OwnedGames_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedGame_GameId",
                table: "OwnedGames",
                newName: "IX_OwnedGames_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameVersion_GameId",
                table: "GameVersions",
                newName: "IX_GameVersions_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAchievements",
                table: "UserAchievements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Screenshots",
                table: "Screenshots",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnedGames",
                table: "OwnedGames",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameVersions",
                table: "GameVersions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTag_Tags_TagsId",
                table: "GameTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameVersions_Game_GameId",
                table: "GameVersions",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedGames_Game_GameId",
                table: "OwnedGames",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedGames_Users_UserId",
                table: "OwnedGames",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Screenshots_Game_GameId",
                table: "Screenshots",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_Achievement_AchievementId",
                table: "UserAchievements",
                column: "AchievementId",
                principalTable: "Achievement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_Users_UserId",
                table: "UserAchievements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTag_Tags_TagsId",
                table: "GameTag");

            migrationBuilder.DropForeignKey(
                name: "FK_GameVersions_Game_GameId",
                table: "GameVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_OwnedGames_Game_GameId",
                table: "OwnedGames");

            migrationBuilder.DropForeignKey(
                name: "FK_OwnedGames_Users_UserId",
                table: "OwnedGames");

            migrationBuilder.DropForeignKey(
                name: "FK_Screenshots_Game_GameId",
                table: "Screenshots");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Achievement_AchievementId",
                table: "UserAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Users_UserId",
                table: "UserAchievements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAchievements",
                table: "UserAchievements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Screenshots",
                table: "Screenshots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnedGames",
                table: "OwnedGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameVersions",
                table: "GameVersions");

            migrationBuilder.RenameTable(
                name: "UserAchievements",
                newName: "UserAchievement");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "Screenshots",
                newName: "Screenshot");

            migrationBuilder.RenameTable(
                name: "OwnedGames",
                newName: "OwnedGame");

            migrationBuilder.RenameTable(
                name: "GameVersions",
                newName: "GameVersion");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchievements_UserId",
                table: "UserAchievement",
                newName: "IX_UserAchievement_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchievements_AchievementId",
                table: "UserAchievement",
                newName: "IX_UserAchievement_AchievementId");

            migrationBuilder.RenameIndex(
                name: "IX_Screenshots_GameId",
                table: "Screenshot",
                newName: "IX_Screenshot_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedGames_UserId",
                table: "OwnedGame",
                newName: "IX_OwnedGame_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedGames_GameId",
                table: "OwnedGame",
                newName: "IX_OwnedGame_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameVersions_GameId",
                table: "GameVersion",
                newName: "IX_GameVersion_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAchievement",
                table: "UserAchievement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Screenshot",
                table: "Screenshot",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnedGame",
                table: "OwnedGame",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameVersion",
                table: "GameVersion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTag_Tag_TagsId",
                table: "GameTag",
                column: "TagsId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameVersion_Game_GameId",
                table: "GameVersion",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedGame_Game_GameId",
                table: "OwnedGame",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedGame_Users_UserId",
                table: "OwnedGame",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Screenshot_Game_GameId",
                table: "Screenshot",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievement_Achievement_AchievementId",
                table: "UserAchievement",
                column: "AchievementId",
                principalTable: "Achievement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievement_Users_UserId",
                table: "UserAchievement",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
