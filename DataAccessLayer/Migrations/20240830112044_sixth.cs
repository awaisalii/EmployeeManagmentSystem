using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class sixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_TasksModel_TaskId",
                table: "Activities");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Activities",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_TasksModel_TaskId",
                table: "Activities",
                column: "TaskId",
                principalTable: "TasksModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_TasksModel_TaskId",
                table: "Activities");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Activities",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_TasksModel_TaskId",
                table: "Activities",
                column: "TaskId",
                principalTable: "TasksModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
