using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class lngchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lng",
                table: "Businesses",
                newName: "lng");

            migrationBuilder.RenameColumn(
                name: "Lat",
                table: "Businesses",
                newName: "lat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lng",
                table: "Businesses",
                newName: "Lng");

            migrationBuilder.RenameColumn(
                name: "lat",
                table: "Businesses",
                newName: "Lat");
        }
    }
}
