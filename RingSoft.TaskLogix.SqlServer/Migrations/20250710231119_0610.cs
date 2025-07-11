using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingSoft.TaskLogix.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class _0610 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PercentComplete",
                table: "Tasks",
                type: "numeric(38,17)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentWidth",
                table: "AdvancedFindColumns",
                type: "numeric(38,17)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,0)");

            migrationBuilder.CreateTable(
                name: "TlTaskRecurYearly",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    RecurType = table.Column<byte>(type: "tinyint", nullable: false),
                    EveryMonthType = table.Column<byte>(type: "tinyint", nullable: true),
                    MonthDay = table.Column<int>(type: "integer", nullable: true),
                    WeekType = table.Column<byte>(type: "tinyint", nullable: true),
                    DayType = table.Column<byte>(type: "tinyint", nullable: true),
                    WeekMonthType = table.Column<byte>(type: "tinyint", nullable: true),
                    RegenYearsAfterCompleted = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TlTaskRecurYearly", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TlTaskRecurYearly_Tasks_TaskId",
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
                name: "TlTaskRecurYearly");

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentComplete",
                table: "Tasks",
                type: "numeric(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(38,17)");

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
