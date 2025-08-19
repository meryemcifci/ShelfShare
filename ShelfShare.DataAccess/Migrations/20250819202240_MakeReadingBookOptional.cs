using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShelfShare.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MakeReadingBookOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Readings_Books_BookId",
                table: "Readings");

            migrationBuilder.AddColumn<int>(
                name: "BookId1",
                table: "Readings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Readings_BookId1",
                table: "Readings",
                column: "BookId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Readings_Books_BookId",
                table: "Readings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Readings_Books_BookId1",
                table: "Readings",
                column: "BookId1",
                principalTable: "Books",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Readings_Books_BookId",
                table: "Readings");

            migrationBuilder.DropForeignKey(
                name: "FK_Readings_Books_BookId1",
                table: "Readings");

            migrationBuilder.DropIndex(
                name: "IX_Readings_BookId1",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "Readings");

            migrationBuilder.AddForeignKey(
                name: "FK_Readings_Books_BookId",
                table: "Readings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
