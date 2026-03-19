using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameNLog.Migrations
{
    /// <inheritdoc />
    public partial class ReviewCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayedAt",
                table: "PlayedGames");

            migrationBuilder.AddColumn<DateTime>(
                name: "LoggedAt",
                table: "PlayedGames",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedAt",
                table: "PlayedGameReviews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoggedAt",
                table: "PlayedGames");

            migrationBuilder.DropColumn(
                name: "ReviewedAt",
                table: "PlayedGameReviews");

            migrationBuilder.AddColumn<int>(
                name: "PlayedAt",
                table: "PlayedGames",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
