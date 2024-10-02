using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class _13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Members",
                table: "PrivateChats");

            migrationBuilder.AlterColumn<int>(
                name: "PrivateChatId",
                table: "Messages",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrivateChatId1",
                table: "Messages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChatUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrivateChatId = table.Column<int>(type: "integer", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatUser_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatUser_PrivateChats_PrivateChatId",
                        column: x => x.PrivateChatId,
                        principalTable: "PrivateChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PrivateChatId1",
                table: "Messages",
                column: "PrivateChatId1");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_ApplicationUserId",
                table: "ChatUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_PrivateChatId",
                table: "ChatUser",
                column: "PrivateChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_PrivateChats_PrivateChatId1",
                table: "Messages",
                column: "PrivateChatId1",
                principalTable: "PrivateChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_PrivateChats_PrivateChatId1",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "ChatUser");

            migrationBuilder.DropIndex(
                name: "IX_Messages_PrivateChatId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PrivateChatId1",
                table: "Messages");

            migrationBuilder.AddColumn<List<string>>(
                name: "Members",
                table: "PrivateChats",
                type: "text[]",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "PrivateChatId",
                table: "Messages",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
