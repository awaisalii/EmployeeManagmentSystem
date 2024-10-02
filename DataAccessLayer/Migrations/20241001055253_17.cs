using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class _17 : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "PrivateChatId",
                table: "Messages",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PrivateChatId",
                table: "ChatUser",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_GroupChats_GroupChatId",
                table: "ChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_PrivateChats_PrivateChatId",
                table: "ChatUser");

            migrationBuilder.AlterColumn<int>(
                name: "PrivateChatId",
                table: "Messages",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrivateChatId",
                table: "ChatUser",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_GroupChats_GroupChatId",
                table: "ChatUser",
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
    }
}
