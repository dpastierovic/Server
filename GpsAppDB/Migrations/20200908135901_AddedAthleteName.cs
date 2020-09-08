using Microsoft.EntityFrameworkCore.Migrations;

namespace GpsAppDB.Migrations
{
    public partial class AddedAthleteName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Application",
                table: "Athletes");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "Application",
                table: "Athletes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "Application",
                table: "Athletes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "Application",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "Application",
                table: "Athletes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Application",
                table: "Athletes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
