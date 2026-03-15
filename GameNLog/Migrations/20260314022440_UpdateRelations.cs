using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameNLog.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCompanies");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Platforms",
                newName: "Abbreviation");

            migrationBuilder.RenameColumn(
                name: "summary",
                table: "Games",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "slug",
                table: "Games",
                newName: "Slug");

            migrationBuilder.AlterColumn<string>(
                name: "ImageID",
                table: "Covers",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Companies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "InvolvedCompanies",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "integer", nullable: false),
                    CompanyID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvolvedCompanies", x => new { x.GameID, x.CompanyID });
                    table.ForeignKey(
                        name: "FK_InvolvedCompanies_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvolvedCompanies_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayedGames_UserID",
                table: "PlayedGames",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayedGameReviews_PlayedGameID",
                table: "PlayedGameReviews",
                column: "PlayedGameID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_CoverID",
                table: "Games",
                column: "CoverID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatforms_PlatformID",
                table: "GamePlatforms",
                column: "PlatformID");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenres_GenreID",
                table: "GameGenres",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_InvolvedCompanies_CompanyID",
                table: "InvolvedCompanies",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenres_Games_GameID",
                table: "GameGenres",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "GameID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenres_Genres_GenreID",
                table: "GameGenres",
                column: "GenreID",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatforms_Games_GameID",
                table: "GamePlatforms",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "GameID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatforms_Platforms_PlatformID",
                table: "GamePlatforms",
                column: "PlatformID",
                principalTable: "Platforms",
                principalColumn: "PlatformID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Covers_CoverID",
                table: "Games",
                column: "CoverID",
                principalTable: "Covers",
                principalColumn: "CoverID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayedGameReviews_PlayedGames_PlayedGameID",
                table: "PlayedGameReviews",
                column: "PlayedGameID",
                principalTable: "PlayedGames",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayedGames_Games_GameID",
                table: "PlayedGames",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "GameID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayedGames_Users_UserID",
                table: "PlayedGames",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGenres_Games_GameID",
                table: "GameGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenres_Genres_GenreID",
                table: "GameGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatforms_Games_GameID",
                table: "GamePlatforms");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatforms_Platforms_PlatformID",
                table: "GamePlatforms");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Covers_CoverID",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayedGameReviews_PlayedGames_PlayedGameID",
                table: "PlayedGameReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayedGames_Games_GameID",
                table: "PlayedGames");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayedGames_Users_UserID",
                table: "PlayedGames");

            migrationBuilder.DropTable(
                name: "InvolvedCompanies");

            migrationBuilder.DropIndex(
                name: "IX_PlayedGames_UserID",
                table: "PlayedGames");

            migrationBuilder.DropIndex(
                name: "IX_PlayedGameReviews_PlayedGameID",
                table: "PlayedGameReviews");

            migrationBuilder.DropIndex(
                name: "IX_Games_CoverID",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_GamePlatforms_PlatformID",
                table: "GamePlatforms");

            migrationBuilder.DropIndex(
                name: "IX_GameGenres_GenreID",
                table: "GameGenres");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "Abbreviation",
                table: "Platforms",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Games",
                newName: "summary");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Games",
                newName: "slug");

            migrationBuilder.AlterColumn<int>(
                name: "ImageID",
                table: "Covers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "GameCompanies",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "integer", nullable: false),
                    CompanyID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCompanies", x => new { x.GameID, x.CompanyID });
                });
        }
    }
}
