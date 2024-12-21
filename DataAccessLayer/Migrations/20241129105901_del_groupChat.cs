using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class del_groupChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_GroupChats_GroupChatId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_GroupChats_GroupChatId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupChatId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GroupChatId1",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_GroupChats_GroupChatId",
                table: "AspNetUsers",
                column: "GroupChatId",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_GroupChats_GroupChatId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "GroupChatId1",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupChatId1",
                table: "AspNetUsers",
                column: "GroupChatId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_GroupChats_GroupChatId",
                table: "AspNetUsers",
                column: "GroupChatId",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_GroupChats_GroupChatId1",
                table: "AspNetUsers",
                column: "GroupChatId1",
                principalTable: "GroupChats",
                principalColumn: "Id");
        }
    }
}
