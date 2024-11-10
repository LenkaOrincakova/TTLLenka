using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LEGO.Inventory.Capacity.Planning.Migrations
{
    public partial class AddTablesforLDC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalDistributionCenters",
                table: "LocalDistributionCenters");

            migrationBuilder.RenameTable(
                name: "LocalDistributionCenters",
                newName: "LocalDistributionCenter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocalDistributionCenter",
                table: "LocalDistributionCenter",
                column: "name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalDistributionCenter",
                table: "LocalDistributionCenter");

            migrationBuilder.RenameTable(
                name: "LocalDistributionCenter",
                newName: "LocalDistributionCenters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocalDistributionCenters",
                table: "LocalDistributionCenters",
                column: "name");
        }
    }
}
