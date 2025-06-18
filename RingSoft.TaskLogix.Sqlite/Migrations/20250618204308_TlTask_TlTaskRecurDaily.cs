using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingSoft.TaskLogix.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class TlTask_TlTaskRecurDaily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TlTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Subject = table.Column<string>(type: "nvarchar", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReminderDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    SnoozeDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    StatusType = table.Column<byte>(type: "smallint", nullable: false),
                    PriorityType = table.Column<byte>(type: "smallint", nullable: false),
                    PercentComplete = table.Column<double>(type: "numeric", nullable: false),
                    RecurType = table.Column<byte>(type: "smallint", nullable: false),
                    RecurStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RecurEndType = table.Column<byte>(type: "smallint", nullable: false),
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
                    RecurType = table.Column<byte>(type: "smallint", nullable: false),
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
        }
    }
}
