using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingSoft.TaskLogix.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class _0620Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TlTaskRecurDaily_TlTask_TaskId",
                table: "TlTaskRecurDaily");

            migrationBuilder.DropForeignKey(
                name: "FK_TlTaskRecurWeekly_TlTask_TaskId",
                table: "TlTaskRecurWeekly");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TlTaskRecurWeekly",
                table: "TlTaskRecurWeekly");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TlTaskRecurDaily",
                table: "TlTaskRecurDaily");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TlTask",
                table: "TlTask");

            migrationBuilder.RenameTable(
                name: "TlTaskRecurWeekly",
                newName: "TaskRecurWeeklys");

            migrationBuilder.RenameTable(
                name: "TlTaskRecurDaily",
                newName: "TaskRecurDailys");

            migrationBuilder.RenameTable(
                name: "TlTask",
                newName: "Tasks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskRecurWeeklys",
                table: "TaskRecurWeeklys",
                column: "TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskRecurDailys",
                table: "TaskRecurDailys",
                column: "TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskRecurDailys_Tasks_TaskId",
                table: "TaskRecurDailys",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskRecurWeeklys_Tasks_TaskId",
                table: "TaskRecurWeeklys",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskRecurDailys_Tasks_TaskId",
                table: "TaskRecurDailys");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskRecurWeeklys_Tasks_TaskId",
                table: "TaskRecurWeeklys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskRecurWeeklys",
                table: "TaskRecurWeeklys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskRecurDailys",
                table: "TaskRecurDailys");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "TlTask");

            migrationBuilder.RenameTable(
                name: "TaskRecurWeeklys",
                newName: "TlTaskRecurWeekly");

            migrationBuilder.RenameTable(
                name: "TaskRecurDailys",
                newName: "TlTaskRecurDaily");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TlTask",
                table: "TlTask",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TlTaskRecurWeekly",
                table: "TlTaskRecurWeekly",
                column: "TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TlTaskRecurDaily",
                table: "TlTaskRecurDaily",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TlTaskRecurDaily_TlTask_TaskId",
                table: "TlTaskRecurDaily",
                column: "TaskId",
                principalTable: "TlTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TlTaskRecurWeekly_TlTask_TaskId",
                table: "TlTaskRecurWeekly",
                column: "TaskId",
                principalTable: "TlTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
