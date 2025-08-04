using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingSoft.TaskLogix.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class TaskHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TlTaskRecurMonthly_Tasks_TaskId",
                table: "TlTaskRecurMonthly");

            migrationBuilder.DropForeignKey(
                name: "FK_TlTaskRecurYearly_Tasks_TaskId",
                table: "TlTaskRecurYearly");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TlTaskRecurYearly",
                table: "TlTaskRecurYearly");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TlTaskRecurMonthly",
                table: "TlTaskRecurMonthly");

            migrationBuilder.RenameTable(
                name: "TlTaskRecurYearly",
                newName: "TaskRecurYearlys");

            migrationBuilder.RenameTable(
                name: "TlTaskRecurMonthly",
                newName: "TaskRecurMonthlys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskRecurYearlys",
                table: "TaskRecurYearlys",
                column: "TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskRecurMonthlys",
                table: "TaskRecurMonthlys",
                column: "TaskId");

            migrationBuilder.CreateTable(
                name: "TaskHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskHistory_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistory_TaskId",
                table: "TaskHistory",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskRecurMonthlys_Tasks_TaskId",
                table: "TaskRecurMonthlys",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskRecurYearlys_Tasks_TaskId",
                table: "TaskRecurYearlys",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskRecurMonthlys_Tasks_TaskId",
                table: "TaskRecurMonthlys");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskRecurYearlys_Tasks_TaskId",
                table: "TaskRecurYearlys");

            migrationBuilder.DropTable(
                name: "TaskHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskRecurYearlys",
                table: "TaskRecurYearlys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskRecurMonthlys",
                table: "TaskRecurMonthlys");

            migrationBuilder.RenameTable(
                name: "TaskRecurYearlys",
                newName: "TlTaskRecurYearly");

            migrationBuilder.RenameTable(
                name: "TaskRecurMonthlys",
                newName: "TlTaskRecurMonthly");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TlTaskRecurYearly",
                table: "TlTaskRecurYearly",
                column: "TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TlTaskRecurMonthly",
                table: "TlTaskRecurMonthly",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TlTaskRecurMonthly_Tasks_TaskId",
                table: "TlTaskRecurMonthly",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TlTaskRecurYearly_Tasks_TaskId",
                table: "TlTaskRecurYearly",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
