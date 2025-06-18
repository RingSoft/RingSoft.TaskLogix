using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingSoft.TaskLogix.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class TlTask_TlTaskRecurDaily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PercentWidth",
                table: "AdvancedFindColumns",
                type: "numeric(38,17)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,0)");

            migrationBuilder.CreateTable(
                name: "TlTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReminderDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    SnoozeDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    StatusType = table.Column<byte>(type: "tinyint", nullable: false),
                    PriorityType = table.Column<byte>(type: "tinyint", nullable: false),
                    PercentComplete = table.Column<decimal>(type: "numeric(38,17)", nullable: false),
                    RecurType = table.Column<byte>(type: "tinyint", nullable: false),
                    RecurStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RecurEndType = table.Column<byte>(type: "tinyint", nullable: false),
                    RecurEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndAfterOccurrences = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TlTask", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TlTaskRecurDaily",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    RecurType = table.Column<byte>(type: "tinyint", nullable: false),
                    RecurDays = table.Column<int>(type: "integer", nullable: true),
                    RegenDaysAfterCompleted = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TlTaskRecurDaily", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TlTaskRecurDaily_TlTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TlTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TlTaskRecurDaily");

            migrationBuilder.DropTable(
                name: "TlTask");

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentWidth",
                table: "AdvancedFindColumns",
                type: "numeric(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(38,17)");
        }
    }
}
