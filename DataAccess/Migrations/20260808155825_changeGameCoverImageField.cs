using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeGameCoverImageField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoverImage",
                table: "Games",
                newName: "CoverImageVertical");

            migrationBuilder.AddColumn<string>(
                name: "CoverImageHorizontal",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CoverImageHorizontal", "CoverImageVertical" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CoverImageHorizontal", "CoverImageVertical" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CoverImageHorizontal", "CoverImageVertical" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageHorizontal",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "CoverImageVertical",
                table: "Games",
                newName: "CoverImage");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "CoverImage",
                value: "https://example.com/covers/cs2.jpg");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "CoverImage",
                value: "https://example.com/covers/neon.jpg");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "CoverImage",
                value: "https://example.com/covers/elden.jpg");
        }
    }
}
