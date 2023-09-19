using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class AddSeriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "moviename",
                table: "episodesWatched",
                newName: "seriesName");

            migrationBuilder.AddColumn<string>(
                name: "episode",
                table: "episodesWatched",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "season",
                table: "episodesWatched",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "episode",
                table: "episodesWatched");

            migrationBuilder.DropColumn(
                name: "season",
                table: "episodesWatched");

            migrationBuilder.RenameColumn(
                name: "seriesName",
                table: "episodesWatched",
                newName: "moviename");
        }
    }
}
