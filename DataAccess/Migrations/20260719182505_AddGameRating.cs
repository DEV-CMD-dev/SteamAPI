using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGameRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "RecommendationPercentage",
                table: "Games",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedReviews",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalReviews",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Rating", "RecommendationPercentage", "RecommendedReviews", "TotalReviews" },
                values: new object[] { 0, 0m, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Rating", "RecommendationPercentage", "RecommendedReviews", "TotalReviews" },
                values: new object[] { 0, 0m, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Rating", "RecommendationPercentage", "RecommendedReviews", "TotalReviews" },
                values: new object[] { 0, 0m, 0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RecommendationPercentage",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RecommendedReviews",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TotalReviews",
                table: "Games");
        }
    }
}
