using Microsoft.EntityFrameworkCore.Migrations;

namespace GpsAppDB.Migrations
{
    public partial class AddedAthleteId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AthleteId",
                schema: "Application",
                table: "Athletes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AthleteId",
                schema: "Application",
                table: "Athletes");
        }
    }
}
