using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VideosTableColumnCamelCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "thumbnail",
                table: "Videos",
                newName: "Thumbnail");

            migrationBuilder.RenameColumn(
                name: "prompt",
                table: "Videos",
                newName: "Prompt");

            migrationBuilder.RenameColumn(
                name: "video",
                table: "Videos",
                newName: "VideoUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Thumbnail",
                table: "Videos",
                newName: "thumbnail");

            migrationBuilder.RenameColumn(
                name: "Prompt",
                table: "Videos",
                newName: "prompt");

            migrationBuilder.RenameColumn(
                name: "VideoUrl",
                table: "Videos",
                newName: "video");
        }
    }
}
