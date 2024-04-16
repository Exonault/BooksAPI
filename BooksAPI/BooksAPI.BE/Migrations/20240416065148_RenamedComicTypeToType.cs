using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksAPI.BE.Migrations
{
    /// <inheritdoc />
    public partial class RenamedComicTypeToType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ComicType",
                table: "LibraryMangas",
                newName: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "LibraryMangas",
                newName: "ComicType");
        }
    }
}
