using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class upgam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Developer_DeveloperId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Publisher_PublisherId",
                table: "Game");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropIndex(
                name: "IX_Game_DeveloperId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_PublisherId",
                table: "Game");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                table: "Game",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeveloperId",
                table: "Game",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DeveloperId1",
                table: "Game",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublisherId1",
                table: "Game",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeveloperId", "DeveloperId1", "PublisherId", "PublisherId1" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeveloperId", "DeveloperId1", "PublisherId", "PublisherId1" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DeveloperId", "DeveloperId1", "PublisherId", "PublisherId1" },
                values: new object[] { null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Game_DeveloperId1",
                table: "Game",
                column: "DeveloperId1");

            migrationBuilder.CreateIndex(
                name: "IX_Game_PublisherId1",
                table: "Game",
                column: "PublisherId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Users_DeveloperId1",
                table: "Game",
                column: "DeveloperId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Users_PublisherId1",
                table: "Game",
                column: "PublisherId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Users_DeveloperId1",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Users_PublisherId1",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_DeveloperId1",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_PublisherId1",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "DeveloperId1",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "PublisherId1",
                table: "Game");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeveloperId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Developer",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Valve" },
                    { 2, "IndieStudio" },
                    { 3, "FromSoftware" }
                });

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeveloperId", "PublisherId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeveloperId", "PublisherId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DeveloperId", "PublisherId" },
                values: new object[] { 3, 3 });

            migrationBuilder.InsertData(
                table: "Publisher",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Valve" },
                    { 2, "IndiePublisher" },
                    { 3, "Bandai Namco" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_DeveloperId",
                table: "Game",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_PublisherId",
                table: "Game",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Developer_DeveloperId",
                table: "Game",
                column: "DeveloperId",
                principalTable: "Developer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Publisher_PublisherId",
                table: "Game",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
