using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GpsAppDB.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Application",
                table: "Activities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2020, 7, 7, 17, 6, 36, 124, DateTimeKind.Local).AddTicks(2906));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Application",
                table: "Activities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2020, 6, 19, 11, 1, 36, 518, DateTimeKind.Local).AddTicks(7649));
        }
    }
}
