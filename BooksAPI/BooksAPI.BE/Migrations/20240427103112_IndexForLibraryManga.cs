using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksAPI.BE.Migrations
{
    /// <inheritdoc />
    public partial class IndexForLibraryManga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LibraryMangas_Id",
                table: "LibraryMangas",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LibraryMangas_Id",
                table: "LibraryMangas");
        }
    }
}
