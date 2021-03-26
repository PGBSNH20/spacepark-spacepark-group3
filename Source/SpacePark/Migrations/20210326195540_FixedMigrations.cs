using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpacePark.Migrations
{
    public partial class FixedMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "departureAt",
                table: "ParkingStatus");

            migrationBuilder.RenameColumn(
                name: "arrivalTime",
                table: "ParkingStatus",
                newName: "ArrivalTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArrivalTime",
                table: "ParkingStatus",
                newName: "arrivalTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "departureAt",
                table: "ParkingStatus",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
