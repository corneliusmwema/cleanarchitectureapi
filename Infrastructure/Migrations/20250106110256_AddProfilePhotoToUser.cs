using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilePhotoToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "Users");
        }
    }
}
