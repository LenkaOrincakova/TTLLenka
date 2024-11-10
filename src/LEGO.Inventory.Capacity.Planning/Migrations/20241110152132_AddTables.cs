using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LEGO.Inventory.Capacity.Planning.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalDistributionCenters",
                columns: table => new
                {
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    regionalDistributionCenterName = table.Column<string>(type: "TEXT", nullable: false),
                    finishedGoodsName = table.Column<string>(type: "TEXT", nullable: false),
                    finishedGoodsStockQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    safetyStockQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    safetyStockThreshold = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalDistributionCenters", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "StockTransportOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FinishedGoodsName = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    RegionalDistributionCenterName = table.Column<string>(type: "TEXT", nullable: false),
                    LocalDistributionCenterName = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransportOrders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalDistributionCenters");

            migrationBuilder.DropTable(
                name: "StockTransportOrders");
        }
    }
}
