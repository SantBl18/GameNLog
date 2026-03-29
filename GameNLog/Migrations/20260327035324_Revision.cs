using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameNLog.Migrations
{
    /// <inheritdoc />
    public partial class Revision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayedGameReviews_GameLog_GameLogId",
                table: "PlayedGameReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayedGameReviews",
                table: "PlayedGameReviews");

            migrationBuilder.RenameTable(
                name: "PlayedGameReviews",
                newName: "GameReviews");

            migrationBuilder.RenameIndex(
                name: "IX_PlayedGameReviews_GameLogId",
                table: "GameReviews",
                newName: "IX_GameReviews_GameLogId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "GameLog",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameReviews",
                table: "GameReviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameReviews_GameLog_GameLogId",
                table: "GameReviews",
                column: "GameLogId",
                principalTable: "GameLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameReviews_GameLog_GameLogId",
                table: "GameReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameReviews",
                table: "GameReviews");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "GameLog");

            migrationBuilder.RenameTable(
                name: "GameReviews",
                newName: "PlayedGameReviews");

            migrationBuilder.RenameIndex(
                name: "IX_GameReviews_GameLogId",
                table: "PlayedGameReviews",
                newName: "IX_PlayedGameReviews_GameLogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayedGameReviews",
                table: "PlayedGameReviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayedGameReviews_GameLog_GameLogId",
                table: "PlayedGameReviews",
                column: "GameLogId",
                principalTable: "GameLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
