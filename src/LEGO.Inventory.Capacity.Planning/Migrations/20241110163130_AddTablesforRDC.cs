using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LEGO.Inventory.Capacity.Planning.Migrations
{
    public partial class AddTablesforRDC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegionalDistributionCenter",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    FinishedGoodsName = table.Column<string>(type: "TEXT", nullable: false),
                    FinishedGoodsStockQuantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionalDistributionCenter", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegionalDistributionCenter");
        }
    }
}
