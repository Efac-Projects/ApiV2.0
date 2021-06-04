using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ContactUs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ContactUs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
