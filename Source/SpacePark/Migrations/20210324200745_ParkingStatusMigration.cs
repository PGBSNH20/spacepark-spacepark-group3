using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SpacePark.Migrations
{
    public partial class ParkingStatusMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    arrivalTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    departureAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CustomerID = table.Column<int>(type: "integer", nullable: false),
                    SpotID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingStatus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ParkingStatus_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkingStatus_Spot_SpotID",
                        column: x => x.SpotID,
                        principalTable: "Spot",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingStatus_CustomerID",
                table: "ParkingStatus",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingStatus_SpotID",
                table: "ParkingStatus",
                column: "SpotID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingStatus");
        }
    }
}
