using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LEGO.Inventory.Capacity.Planning.Migrations
{
    public partial class AddTablesforGoodsReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoodsReceipts",
                columns: table => new
                {
                    StockTransportOrderId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceipts", x => x.StockTransportOrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsReceipts");
        }
    }
}
