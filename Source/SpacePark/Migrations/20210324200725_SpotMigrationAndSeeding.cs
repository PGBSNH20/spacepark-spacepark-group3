using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SpacePark.Migrations
{
    public partial class SpotMigrationAndSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Spot",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Spot", x => x.ID));

            migrationBuilder.InsertData(
                table: "Spot",
                columns: new[] { "ID", "Price", "Size" },
                values: new object[,]
                {
                    { 1, 120m, 20 },
                    { 2, 120m, 20 },
                    { 3, 280m, 50 },
                    { 4, 280m, 50 },
                    { 5, 280m, 50 },
                    { 6, 600m, 100 },
                    { 7, 600m, 100 },
                    { 8, 1600m, 100 },
                    { 9, 8000m, 1000 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spot");
        }
    }
}
