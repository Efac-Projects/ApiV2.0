using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class changethreatentsce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "ContactUs");

            migrationBuilder.AddColumn<string>(
                name: "Availability",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Treatments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeFrom",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeTo",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "TimeFrom",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "TimeTo",
                table: "Treatments");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Treatments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Treatments",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "ContactUs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
