using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameNLog.Migrations
{
    /// <inheritdoc />
    public partial class NameIndex1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Games_Name",
                table: "Games");
            migrationBuilder.Sql("CREATE INDEX games_name_fts ON \"Games\" USING GIN (to_tsvector('english', \"Name\"));");
            migrationBuilder.Sql("CREATE INDEX games_name_trgm ON \"Games\" USING GIN (\"Name\" gin_trgm_ops);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Games_Name",
                table: "Games",
                column: "Name")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });
        }
    }
}
