using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrelloClone.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModelsAndRelationshipsV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardLists_Boards_KanbanBoardId",
                table: "BoardLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_AuthorUsername",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "AuthorUsername",
                table: "Comments",
                newName: "AuthorName");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AuthorUsername",
                table: "Comments",
                newName: "IX_Comments_AuthorName");

            migrationBuilder.RenameColumn(
                name: "KanbanBoardId",
                table: "BoardLists",
                newName: "BoardId");

            migrationBuilder.RenameIndex(
                name: "IX_BoardLists_KanbanBoardId",
                table: "BoardLists",
                newName: "IX_BoardLists_BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardLists_Boards_BoardId",
                table: "BoardLists",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_AuthorName",
                table: "Comments",
                column: "AuthorName",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardLists_Boards_BoardId",
                table: "BoardLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_AuthorName",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "AuthorName",
                table: "Comments",
                newName: "AuthorUsername");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AuthorName",
                table: "Comments",
                newName: "IX_Comments_AuthorUsername");

            migrationBuilder.RenameColumn(
                name: "BoardId",
                table: "BoardLists",
                newName: "KanbanBoardId");

            migrationBuilder.RenameIndex(
                name: "IX_BoardLists_BoardId",
                table: "BoardLists",
                newName: "IX_BoardLists_KanbanBoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardLists_Boards_KanbanBoardId",
                table: "BoardLists",
                column: "KanbanBoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_AuthorUsername",
                table: "Comments",
                column: "AuthorUsername",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
