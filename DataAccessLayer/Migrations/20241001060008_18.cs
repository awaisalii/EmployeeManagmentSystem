using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class _18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_GroupChats_GroupChatId",
                table: "ChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_PrivateChats_PrivateChatId",
                table: "ChatUser");

            migrationBuilder.DropIndex(
                name: "IX_ChatUser_GroupChatId",
                table: "ChatUser");

            migrationBuilder.DropColumn(
                name: "GroupChatId",
                table: "ChatUser");

            migrationBuilder.AlterColumn<int>(
                name: "PrivateChatId",
                table: "ChatUser",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupChatId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupChatId",
                table: "AspNetUsers",
                column: "GroupChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_GroupChats_GroupChatId",
                table: "AspNetUsers",
                column: "GroupChatId",
                principalTable: "GroupChats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_PrivateChats_PrivateChatId",
                table: "ChatUser",
                column: "PrivateChatId",
                principalTable: "PrivateChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_GroupChats_GroupChatId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_PrivateChats_PrivateChatId",
                table: "ChatUser");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupChatId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GroupChatId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "PrivateChatId",
                table: "ChatUser",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "GroupChatId",
                table: "ChatUser",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_GroupChatId",
                table: "ChatUser",
                column: "GroupChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_GroupChats_GroupChatId",
                table: "ChatUser",
                column: "GroupChatId",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_PrivateChats_PrivateChatId",
                table: "ChatUser",
                column: "PrivateChatId",
                principalTable: "PrivateChats",
                principalColumn: "Id");
        }
    }
}
