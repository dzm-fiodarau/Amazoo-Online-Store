using Microsoft.EntityFrameworkCore.Migrations;

namespace AmazooApp.Migrations
{
    public partial class addHomeAddressToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "zipcode",
                table: "AspNetUsers",
                newName: "Zipcode");

            migrationBuilder.RenameColumn(
                name: "province",
                table: "AspNetUsers",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "AspNetUsers",
                newName: "City");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Zipcode",
                table: "AspNetUsers",
                newName: "zipcode");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "AspNetUsers",
                newName: "province");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "AspNetUsers",
                newName: "city");
        }
    }
}
