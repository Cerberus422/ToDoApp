using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserOwnership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ToDo",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Category",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ToDo_AppUserId",
                table: "ToDo",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_AppUserId",
                table: "Category",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_AppUserId",
                table: "Category",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDo_AspNetUsers_AppUserId",
                table: "ToDo",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_AppUserId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDo_AspNetUsers_AppUserId",
                table: "ToDo");

            migrationBuilder.DropIndex(
                name: "IX_ToDo_AppUserId",
                table: "ToDo");

            migrationBuilder.DropIndex(
                name: "IX_Category_AppUserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ToDo");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Category");
        }
    }
}
