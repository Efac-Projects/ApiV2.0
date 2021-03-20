using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class addappoinmenttreatments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Treatments_TreatmentId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_TreatmentId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TreatmentId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ThreatmentId",
                table: "Appointments",
                column: "ThreatmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Treatments_ThreatmentId",
                table: "Appointments",
                column: "ThreatmentId",
                principalTable: "Treatments",
                principalColumn: "TreatmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Treatments_ThreatmentId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ThreatmentId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "TreatmentId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TreatmentId",
                table: "Appointments",
                column: "TreatmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Treatments_TreatmentId",
                table: "Appointments",
                column: "TreatmentId",
                principalTable: "Treatments",
                principalColumn: "TreatmentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
