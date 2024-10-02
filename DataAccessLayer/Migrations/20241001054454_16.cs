using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class _16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Members",
                table: "GroupChats");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "GroupChats",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TaskModelId",
                table: "GroupChats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupChatId",
                table: "ChatUser",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupChats_TaskModelId",
                table: "GroupChats",
                column: "TaskModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_GroupChatId",
                table: "ChatUser",
                column: "GroupChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_GroupChats_GroupChatId",
                table: "ChatUser",
                column: "GroupChatId",
                principalTable: "GroupChats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChats_TasksModel_TaskModelId",
                table: "GroupChats",
                column: "TaskModelId",
                principalTable: "TasksModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_GroupChats_GroupChatId",
                table: "ChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupChats_TasksModel_TaskModelId",
                table: "GroupChats");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_GroupChats_TaskModelId",
                table: "GroupChats");

            migrationBuilder.DropIndex(
                name: "IX_ChatUser_GroupChatId",
                table: "ChatUser");

            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "GroupChats");

            migrationBuilder.DropColumn(
                name: "TaskModelId",
                table: "GroupChats");

            migrationBuilder.DropColumn(
                name: "GroupChatId",
                table: "ChatUser");

            migrationBuilder.AddColumn<List<string>>(
                name: "Members",
                table: "GroupChats",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
