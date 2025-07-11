using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingSoft.TaskLogix.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class _0610 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TlTaskRecurYearly",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    RecurType = table.Column<byte>(type: "smallint", nullable: false),
                    EveryMonthType = table.Column<byte>(type: "smallint", nullable: true),
                    MonthDay = table.Column<int>(type: "integer", nullable: true),
                    WeekType = table.Column<byte>(type: "smallint", nullable: true),
                    DayType = table.Column<byte>(type: "smallint", nullable: true),
                    WeekMonthType = table.Column<byte>(type: "smallint", nullable: true),
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
        }
    }
}
