using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace GpsAppDB.Migrations
{
    public partial class AddedAthleteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Application.Activities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activities",
                schema: "Application",
                table: "Activities");

            migrationBuilder.DeleteData(
                schema: "Application",
                table: "Activities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Gear",
                schema: "Application",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "StartingPoint",
                schema: "Application",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                schema: "Application",
                table: "Activities");

            migrationBuilder.RenameTable(
                name: "Activities",
                schema: "Application",
                newName: "Athletes",
                newSchema: "Application");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Athletes",
                schema: "Application",
                table: "Athletes",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Athletes",
                schema: "Application",
                table: "Athletes");

            migrationBuilder.RenameTable(
                name: "Athletes",
                schema: "Application",
                newName: "Activities",
                newSchema: "Application");

            migrationBuilder.AddColumn<string>(
                name: "Gear",
                schema: "Application",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "StartingPoint",
                schema: "Application",
                table: "Activities",
                type: "geography",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                schema: "Application",
                table: "Activities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activities",
                schema: "Application",
                table: "Activities",
                column: "Id");

            migrationBuilder.InsertData(
                schema: "Application",
                table: "Activities",
                columns: new[] { "Id", "Gear", "Name", "StartingPoint", "Timestamp" },
                values: new object[] { 1, "CUBE Agree C62 Race Disc", "seed activity", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (48 18)"), new DateTime(2020, 7, 7, 17, 6, 36, 124, DateTimeKind.Local).AddTicks(2906) });
        }
    }
}
