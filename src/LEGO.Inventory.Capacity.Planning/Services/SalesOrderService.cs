using LEGO.Inventory.Capacity.Planning.Domain.Orders;
using LEGO.Inventory.Capacity.Planning.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LEGO.Inventory.Capacity.Planning.Services
{
    public class SalesOrderService: ISalesOrderService
    {
        private readonly ILogger<SalesOrderService> _logger;
        private readonly ApiDbContext _context;

        
        public SalesOrderService( ILogger<SalesOrderService> logger, ApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<SalesOrder>> GetSalesOrdersAsync()
        {
            
            // var LDC =await  _context.LocalDistributionCenters.AddAsync("Central Warehouse Europe", "LEGO European Distribution Center", "Lego - Harry Potter", 50, 20, 20);
             var ordersDb = await _context.SalesOrders.ToListAsync(); 
            return await Task.FromResult(ordersDb);
        }

        public async Task CreateSalesOrderAsync(SalesOrder salesOrder)
        {
            var _localDistributionCenter =  _context.LocalDistributionCenter.FirstOrDefault(ldc => ldc.name == salesOrder.LocalDistributionCenterName);
    
            if (_localDistributionCenter == null)
            {
                _logger.LogError("invalid local distribution center name");
                throw new Exception("invalid local distribution center name");
            }
            else
            {
                await _context.SalesOrders.AddAsync(salesOrder);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"new order created: " + salesOrder.FinishedGoodsName + " : " + salesOrder.Quantity + " -LDC: " + salesOrder.LocalDistributionCenterName);
            }
        }

    }
}
