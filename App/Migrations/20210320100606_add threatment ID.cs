using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class addthreatmentID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Treatments_ThreatmentTreatmentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Businesses_BusinessId",
                table: "Treatments");

            migrationBuilder.RenameColumn(
                name: "ThreatmentTreatmentId",
                table: "Appointments",
                newName: "TreatmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ThreatmentTreatmentId",
                table: "Appointments",
                newName: "IX_Appointments_TreatmentId");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                table: "Treatments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThreatmentId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Treatments_TreatmentId",
                table: "Appointments",
                column: "TreatmentId",
                principalTable: "Treatments",
                principalColumn: "TreatmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Businesses_BusinessId",
                table: "Treatments",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "BusinessId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Treatments_TreatmentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Businesses_BusinessId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "ThreatmentId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "TreatmentId",
                table: "Appointments",
                newName: "ThreatmentTreatmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_TreatmentId",
                table: "Appointments",
                newName: "IX_Appointments_ThreatmentTreatmentId");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                table: "Treatments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Treatments_ThreatmentTreatmentId",
                table: "Appointments",
                column: "ThreatmentTreatmentId",
                principalTable: "Treatments",
                principalColumn: "TreatmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Businesses_BusinessId",
                table: "Treatments",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "BusinessId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
