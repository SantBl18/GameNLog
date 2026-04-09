using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameNLog.Migrations
{
    /// <inheritdoc />
    public partial class AddComputedBayesianAverage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "Game",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "Game",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "BayesianRating",
                table: "Game",
                type: "double precision",
                nullable: true,
                computedColumnSql: "(\r\n                    (\"RatingCount\"::double precision / (\"RatingCount\" + 20)) * COALESCE(\"AverageRating\", 0) +\r\n                (20 / (\"RatingCount\" + 20)) * 6.5\r\n                )",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_BayesianRating",
                table: "Game",
                column: "BayesianRating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Game_BayesianRating",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "BayesianRating",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Game");
        }
    }
}
