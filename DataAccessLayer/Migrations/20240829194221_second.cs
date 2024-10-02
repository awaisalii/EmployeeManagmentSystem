using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_TasksModel_TaskId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_TasksModel_AspNetUsers_ApplicationUserId",
                table: "TasksModel");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "TasksModel",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Notes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_TasksModel_TaskId",
                table: "Notes",
                column: "TaskId",
                principalTable: "TasksModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TasksModel_AspNetUsers_ApplicationUserId",
                table: "TasksModel",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_TasksModel_TaskId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_TasksModel_AspNetUsers_ApplicationUserId",
                table: "TasksModel");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "TasksModel",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Notes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_TasksModel_TaskId",
                table: "Notes",
                column: "TaskId",
                principalTable: "TasksModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TasksModel_AspNetUsers_ApplicationUserId",
                table: "TasksModel",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
