using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace GpsAppDB.Migrations
{
    public partial class CustomPointImplementation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Markers");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Markers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Markers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Radius",
                table: "Markers",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "Radius",
                table: "Markers");

            migrationBuilder.AddColumn<Geometry>(
                name: "Area",
                table: "Markers",
                type: "geography",
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "Position",
                table: "Markers",
                type: "geography",
                nullable: true);
        }
    }
}
