using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BooksAPI.BE.Migrations
{
    /// <inheritdoc />
    public partial class RemovedChapters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9aebb97-9029-4d92-8972-d40d9eeb948f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f66f031e-5eed-45ff-b3c0-c4f43b195fdd");

            migrationBuilder.DropColumn(
                name: "ReadChapters",
                table: "UserComics");

            migrationBuilder.DropColumn(
                name: "TotalChapters",
                table: "LibraryComics");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "284b3a5c-4235-4b01-ba23-09f2f6f9737c", null, "User", "USER" },
                    { "3187bce0-f9a9-48fb-adb6-36cea86dfb16", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "284b3a5c-4235-4b01-ba23-09f2f6f9737c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3187bce0-f9a9-48fb-adb6-36cea86dfb16");

            migrationBuilder.AddColumn<int>(
                name: "ReadChapters",
                table: "UserComics",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalChapters",
                table: "LibraryComics",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e9aebb97-9029-4d92-8972-d40d9eeb948f", null, "Admin", "ADMIN" },
                    { "f66f031e-5eed-45ff-b3c0-c4f43b195fdd", null, "User", "USER" }
                });
        }
    }
}
