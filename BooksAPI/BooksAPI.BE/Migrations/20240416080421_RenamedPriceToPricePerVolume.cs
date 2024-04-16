using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksAPI.BE.Migrations
{
    /// <inheritdoc />
    public partial class RenamedPriceToPricePerVolume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "UserMangas",
                newName: "PricePerVolume");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerVolume",
                table: "UserMangas",
                newName: "Price");
        }
    }
}
