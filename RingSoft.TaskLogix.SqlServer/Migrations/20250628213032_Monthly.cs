using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingSoft.TaskLogix.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Monthly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecurStartDate",
                table: "Tasks");

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentComplete",
                table: "Tasks",
                type: "numeric(38,17)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,0)");

            migrationBuilder.AddColumn<bool>(
                name: "IsDismissed",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Tasks",
                type: "ntext",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentWidth",
                table: "AdvancedFindColumns",
                type: "numeric(38,17)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,0)");

            migrationBuilder.CreateTable(
                name: "TlTaskRecurMonthly",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    RecurType = table.Column<byte>(type: "tinyint", nullable: false),
                    DayXOfEvery = table.Column<int>(type: "integer", nullable: true),
                    OfEveryYMonths = table.Column<int>(type: "integer", nullable: true),
                    WeekType = table.Column<byte>(type: "tinyint", nullable: true),
                    DayType = table.Column<byte>(type: "tinyint", nullable: true),
                    OfEveryWeekTypeMonths = table.Column<int>(type: "integer", nullable: true),
                    RegenMonthsAfterCompleted = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TlTaskRecurMonthly", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TlTaskRecurMonthly_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TlTaskRecurMonthly");

            migrationBuilder.DropColumn(
                name: "IsDismissed",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Tasks");

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentComplete",
                table: "Tasks",
                type: "numeric(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(38,17)");

            migrationBuilder.AddColumn<DateTime>(
                name: "RecurStartDate",
                table: "Tasks",
                type: "datetime",
                nullable: true);

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
