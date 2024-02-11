using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElmTest.Migrations
{
    public partial class updateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Book_LastModified",
                table: "Book");

            migrationBuilder.CreateIndex(
                name: "IX_Book_BookId",
                table: "Book",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Book_BookId",
                table: "Book");

            migrationBuilder.CreateIndex(
                name: "IX_Book_LastModified",
                table: "Book",
                column: "LastModified");
        }
    }
}
