using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShelfShare.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBookGroupRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_AddedByUserId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Group_GroupId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_GroupId",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "BookGroups",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGroups", x => new { x.BookId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_BookGroups_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGroups_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookGroups_GroupId",
                table: "BookGroups",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_AddedByUserId",
                table: "Books",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_AddedByUserId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookGroups");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GroupId",
                table: "Books",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_AddedByUserId",
                table: "Books",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Group_GroupId",
                table: "Books",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id");
        }
    }
}
