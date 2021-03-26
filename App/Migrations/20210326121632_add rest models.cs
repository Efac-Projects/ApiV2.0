using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class addrestmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_OpeningHours_OpeningHoursId",
                table: "Businesses");

            migrationBuilder.DropTable(
                name: "TimeRange");

            migrationBuilder.DropTable(
                name: "Time");

            migrationBuilder.DropTable(
                name: "WorkDays");

            migrationBuilder.DropTable(
                name: "OpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_OpeningHoursId",
                table: "Businesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "OpeningHoursId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "Timezone",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "NotificationBars");

            migrationBuilder.RenameTable(
                name: "Contact",
                newName: "ContactUs");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Treatments",
                newName: "Specification");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationBars",
                table: "NotificationBars",
                column: "NotificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactUs",
                table: "ContactUs",
                column: "ContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationBars",
                table: "NotificationBars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactUs",
                table: "ContactUs");

            migrationBuilder.RenameTable(
                name: "NotificationBars",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "ContactUs",
                newName: "Contact");

            migrationBuilder.RenameColumn(
                name: "Specification",
                table: "Treatments",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "OpeningHoursId",
                table: "Businesses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Timezone",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "NotificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                table: "Contact",
                column: "ContactId");

            migrationBuilder.CreateTable(
                name: "OpeningHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Time",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    Second = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Time", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    OpeningHourId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkDays_OpeningHours_OpeningHourId",
                        column: x => x.OpeningHourId,
                        principalTable: "OpeningHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeRange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndTimeId = table.Column<int>(type: "int", nullable: true),
                    StartTimeId = table.Column<int>(type: "int", nullable: true),
                    WorkDayId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeRange_Time_EndTimeId",
                        column: x => x.EndTimeId,
                        principalTable: "Time",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeRange_Time_StartTimeId",
                        column: x => x.StartTimeId,
                        principalTable: "Time",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeRange_WorkDays_WorkDayId",
                        column: x => x.WorkDayId,
                        principalTable: "WorkDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_OpeningHoursId",
                table: "Businesses",
                column: "OpeningHoursId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRange_EndTimeId",
                table: "TimeRange",
                column: "EndTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRange_StartTimeId",
                table: "TimeRange",
                column: "StartTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRange_WorkDayId",
                table: "TimeRange",
                column: "WorkDayId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkDays_OpeningHourId",
                table: "WorkDays",
                column: "OpeningHourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_OpeningHours_OpeningHoursId",
                table: "Businesses",
                column: "OpeningHoursId",
                principalTable: "OpeningHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
