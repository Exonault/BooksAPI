using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksAPI.BE.Migrations
{
    /// <inheritdoc />
    public partial class AddMainImageUrlColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImageUrl",
                table: "LibraryMangas",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImageUrl",
                table: "LibraryMangas");
        }
    }
}
