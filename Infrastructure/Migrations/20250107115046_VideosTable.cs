using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VideosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Videos");

            migrationBuilder.AddColumn<string>(
                name: "prompt",
                table: "Videos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "thumbnail",
                table: "Videos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "video",
                table: "Videos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "prompt",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "thumbnail",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "video",
                table: "Videos");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Videos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Videos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Videos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Videos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Videos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
