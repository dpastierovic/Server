using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace GpsAppDB.Migrations
{
    public partial class AddedSpatialDataToMarker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Markers");

            migrationBuilder.AddColumn<Geometry>(
                name: "Area",
                table: "Markers",
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "Position",
                table: "Markers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Markers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
