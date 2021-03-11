using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class Removechangeimagehandlercoloumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerPath",
                table: "Businesses");

            migrationBuilder.RenameColumn(
                name: "CardImagePath",
                table: "Businesses",
                newName: "ImageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Businesses",
                newName: "CardImagePath");

            migrationBuilder.AddColumn<string>(
                name: "BannerPath",
                table: "Businesses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
