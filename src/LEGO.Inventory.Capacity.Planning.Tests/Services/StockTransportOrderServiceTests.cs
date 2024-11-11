using System;
using System.Linq;
using LEGO.Inventory.Capacity.Planning.Domain;
using LEGO.Inventory.Capacity.Planning.Domain.DistributionCenters;
using LEGO.Inventory.Capacity.Planning.Services;
using LEGO.Inventory.Capacity.Planning.Services.Interfaces;
using LEGO.Inventory.Capacity.Planning.Storage;
using Xunit;

namespace LEGO.Inventory.Capacity.Planning.Tests.Services
{
    public class StockTransportOrderServiceTests
    {
        private readonly ApiDbContext _context;
        private readonly IStockTransportOrderService _stockTransportOrderService;
        public StockTransportOrderServiceTests(ApiDbContext context) 
        {
            // _storage = new Storage.Storage();
            _stockTransportOrderService = new StockTransportOrderService(_context);
            _context = context;

        }
        [Fact]
        public void GetStockTransportOrdersByLDC_ShouldReturnMatchingOrders()
        {
            // Arrange

            var ldcName = "Central Warehouse Europe";
            var sto1 = new StockTransportOrder("Lego - Harry Potter", 10, "LEGO European Distribution Center", ldcName);
            var sto2 = new StockTransportOrder("Lego - Star Wars", 15, "LEGO European Distribution Center", ldcName);
            var sto3 = new StockTransportOrder("Lego - Ninjago", 5, "LEGO European Distribution Center", "Other LDC");

            _context.StockTransportOrders.AddRange(new[] { sto1, sto2, sto3 });

            // Act
            var result = _stockTransportOrderService.GetStockTransportOrdersByLDCAsync(ldcName);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(sto1, result);
            Assert.Contains(sto2, result);
            Assert.DoesNotContain(sto3, result);
        }

        [Fact]
        public void CreateStockTransportOrder_ShouldAddOrderToStorage()
        {
            // Arrange

            var sto = new StockTransportOrder("Lego - Star Wars", 10, "LEGO European Distribution Center", "Western Warehouse Europe");

            // Act
            _stockTransportOrderService.CreateStockTransportOrderAsync(sto);

            // Assert
            Assert.Single(_context.StockTransportOrders);
            Assert.Contains(sto, _context.StockTransportOrders);
        }

    }
}
