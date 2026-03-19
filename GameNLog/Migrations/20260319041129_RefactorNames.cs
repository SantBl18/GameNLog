using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameNLog.Migrations
{
    /// <inheritdoc />
    public partial class RefactorNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_InvolvedCompanies_Companies_CompanyID",
                table: "InvolvedCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_InvolvedCompanies_Games_GameID",
                table: "InvolvedCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayedGameReviews_PlayedGames_PlayedGameID",
                table: "PlayedGameReviews");

            migrationBuilder.DropTable(
                name: "PlayedGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Platforms",
                table: "Platforms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvolvedCompanies",
                table: "InvolvedCompanies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamePlatforms",
                table: "GamePlatforms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameGenres",
                table: "GameGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Covers",
                table: "Covers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Platforms",
                newName: "Platform");

            migrationBuilder.RenameTable(
                name: "InvolvedCompanies",
                newName: "InvolvedCompany");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameTable(
                name: "GamePlatforms",
                newName: "GamePlatform");

            migrationBuilder.RenameTable(
                name: "GameGenres",
                newName: "GameGenre");

            migrationBuilder.RenameTable(
                name: "Covers",
                newName: "Cover");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "Company");

            migrationBuilder.RenameColumn(
                name: "PlayedGameID",
                table: "PlayedGameReviews",
                newName: "GameLogId");

            migrationBuilder.RenameColumn(
                name: "ReviewID",
                table: "PlayedGameReviews",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_PlayedGameReviews_PlayedGameID",
                table: "PlayedGameReviews",
                newName: "IX_PlayedGameReviews_GameLogId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PlatformID",
                table: "Platform",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_InvolvedCompanies_CompanyID",
                table: "InvolvedCompany",
                newName: "IX_InvolvedCompany_CompanyID");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Genre",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GameID",
                table: "Game",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Games_CoverID",
                table: "Game",
                newName: "IX_Game_CoverID");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlatforms_PlatformID",
                table: "GamePlatform",
                newName: "IX_GamePlatform_PlatformID");

            migrationBuilder.RenameIndex(
                name: "IX_GameGenres_GenreID",
                table: "GameGenre",
                newName: "IX_GameGenre_GenreID");

            migrationBuilder.RenameColumn(
                name: "CoverID",
                table: "Cover",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                table: "Company",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Platform",
                table: "Platform",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvolvedCompany",
                table: "InvolvedCompany",
                columns: new[] { "GameID", "CompanyID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamePlatform",
                table: "GamePlatform",
                columns: new[] { "GameID", "PlatformID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameGenre",
                table: "GameGenre",
                columns: new[] { "GameID", "GenreID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cover",
                table: "Cover",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GameLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    GameID = table.Column<int>(type: "integer", nullable: false),
                    LoggedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameLog_Game_GameID",
                        column: x => x.GameID,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameLog_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameLog_GameID_UserID",
                table: "GameLog",
                columns: new[] { "GameID", "UserID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameLog_UserID",
                table: "GameLog",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Cover_CoverID",
                table: "Game",
                column: "CoverID",
                principalTable: "Cover",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Game_GameID",
                table: "GameGenre",
                column: "GameID",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Genre_GenreID",
                table: "GameGenre",
                column: "GenreID",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatform_Game_GameID",
                table: "GamePlatform",
                column: "GameID",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatform_Platform_PlatformID",
                table: "GamePlatform",
                column: "PlatformID",
                principalTable: "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvolvedCompany_Company_CompanyID",
                table: "InvolvedCompany",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvolvedCompany_Game_GameID",
                table: "InvolvedCompany",
                column: "GameID",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayedGameReviews_GameLog_GameLogId",
                table: "PlayedGameReviews",
                column: "GameLogId",
                principalTable: "GameLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Cover_CoverID",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Game_GameID",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Genre_GenreID",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatform_Game_GameID",
                table: "GamePlatform");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatform_Platform_PlatformID",
                table: "GamePlatform");

            migrationBuilder.DropForeignKey(
                name: "FK_InvolvedCompany_Company_CompanyID",
                table: "InvolvedCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_InvolvedCompany_Game_GameID",
                table: "InvolvedCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayedGameReviews_GameLog_GameLogId",
                table: "PlayedGameReviews");

            migrationBuilder.DropTable(
                name: "GameLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Platform",
                table: "Platform");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvolvedCompany",
                table: "InvolvedCompany");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamePlatform",
                table: "GamePlatform");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameGenre",
                table: "GameGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cover",
                table: "Cover");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                table: "Company");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Platform",
                newName: "Platforms");

            migrationBuilder.RenameTable(
                name: "InvolvedCompany",
                newName: "InvolvedCompanies");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "GamePlatform",
                newName: "GamePlatforms");

            migrationBuilder.RenameTable(
                name: "GameGenre",
                newName: "GameGenres");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameTable(
                name: "Cover",
                newName: "Covers");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "Companies");

            migrationBuilder.RenameColumn(
                name: "GameLogId",
                table: "PlayedGameReviews",
                newName: "PlayedGameID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PlayedGameReviews",
                newName: "ReviewID");

            migrationBuilder.RenameIndex(
                name: "IX_PlayedGameReviews_GameLogId",
                table: "PlayedGameReviews",
                newName: "IX_PlayedGameReviews_PlayedGameID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Platforms",
                newName: "PlatformID");

            migrationBuilder.RenameIndex(
                name: "IX_InvolvedCompany_CompanyID",
                table: "InvolvedCompanies",
                newName: "IX_InvolvedCompanies_CompanyID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Genres",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlatform_PlatformID",
                table: "GamePlatforms",
                newName: "IX_GamePlatforms_PlatformID");

            migrationBuilder.RenameIndex(
                name: "IX_GameGenre_GenreID",
                table: "GameGenres",
                newName: "IX_GameGenres_GenreID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Games",
                newName: "GameID");

            migrationBuilder.RenameIndex(
                name: "IX_Game_CoverID",
                table: "Games",
                newName: "IX_Games_CoverID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Covers",
                newName: "CoverID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Companies",
                newName: "CompanyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Platforms",
                table: "Platforms",
                column: "PlatformID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvolvedCompanies",
                table: "InvolvedCompanies",
                columns: new[] { "GameID", "CompanyID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamePlatforms",
                table: "GamePlatforms",
                columns: new[] { "GameID", "PlatformID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameGenres",
                table: "GameGenres",
                columns: new[] { "GameID", "GenreID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "GameID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Covers",
                table: "Covers",
                column: "CoverID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "CompanyID");

            migrationBuilder.CreateTable(
                name: "PlayedGames",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    GameID = table.Column<int>(type: "integer", nullable: false),
                    LoggedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayedGames", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlayedGames_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayedGames_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayedGames_GameID_UserID",
                table: "PlayedGames",
                columns: new[] { "GameID", "UserID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayedGames_UserID",
                table: "PlayedGames",
                column: "UserID");

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
                name: "FK_InvolvedCompanies_Companies_CompanyID",
                table: "InvolvedCompanies",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvolvedCompanies_Games_GameID",
                table: "InvolvedCompanies",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "GameID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayedGameReviews_PlayedGames_PlayedGameID",
                table: "PlayedGameReviews",
                column: "PlayedGameID",
                principalTable: "PlayedGames",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
