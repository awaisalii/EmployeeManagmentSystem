using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class _15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_PrivateChats_PrivateChatId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_PrivateChatId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PrivateChatId1",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "Messages",
                newName: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SenderId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Messages",
                newName: "User");

            migrationBuilder.AddColumn<int>(
                name: "PrivateChatId1",
                table: "Messages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PrivateChatId1",
                table: "Messages",
                column: "PrivateChatId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_PrivateChats_PrivateChatId1",
                table: "Messages",
                column: "PrivateChatId1",
                principalTable: "PrivateChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
