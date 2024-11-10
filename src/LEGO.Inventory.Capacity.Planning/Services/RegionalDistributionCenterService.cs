using LEGO.Inventory.Capacity.Planning.Domain;
using LEGO.Inventory.Capacity.Planning.Domain.DistributionCenters;
using LEGO.Inventory.Capacity.Planning.Domain.GoodsMovement;
using LEGO.Inventory.Capacity.Planning.Services.Interfaces;
using LEGO.Inventory.Capacity.Planning.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace LEGO.Inventory.Capacity.Planning.Services
{
    public class RegionalDistributionCenterService : IRegionalDistributionCenterService
    {
        private readonly IStorage _storage;
        private readonly ApiDbContext _context;

        private readonly ILogger<RegionalDistributionCenterService> _logger;
        
        public RegionalDistributionCenterService(IStorage storage, ILogger<RegionalDistributionCenterService> logger, ApiDbContext context)
        {
            _storage = storage;
            _logger = logger;
            _context = context;
        }

        public int TryPickSTO(Guid stockTransportOrderId)
        {   

            var result = _context.RegionalDistributionCenter.Single(b => b.Name == b.Name);
        
            var stockTransportOrder = _context.StockTransportOrders.FirstOrDefault(sto => sto.Id == stockTransportOrderId) ?? throw new Exception("Missing stock transport order");
            if (stockTransportOrder.Quantity > _context.RegionalDistributionCenter.Select(a=>a.FinishedGoodsStockQuantity).FirstOrDefault())

            {
                _logger.LogError($@"Couldn't pick stock transport order {stockTransportOrder.Id}. Insufficient stock for product {_context.RegionalDistributionCenter.Select(a=>a.FinishedGoodsName).ToString()}.
Ordered stock: {stockTransportOrder.Quantity}, current stock: {_context.RegionalDistributionCenter.Select(a=>a.FinishedGoodsStockQuantity).ToString()}");
                throw new Exception(
    $@"Couldn't pick stock transport order {stockTransportOrder.Id}. Insufficient stock for product {_context.RegionalDistributionCenter.Select(a=>a.FinishedGoodsName)}.
Ordered stock: {stockTransportOrder.Quantity}, current stock: {_context.RegionalDistributionCenter.Select(a=>a.FinishedGoodsStockQuantity)}");
                
            }

            stockTransportOrder.UpdateStatus(StockTransportOrderStatus.Picked);

           var newQuantity= _context.RegionalDistributionCenter.Select(a=>a.FinishedGoodsStockQuantity).First();
           var result2 = newQuantity - stockTransportOrder.Quantity;
            result.UpdateQuantity(result2);
            _context.RegionalDistributionCenter.Update(result);
            _context.SaveChanges();

            return stockTransportOrder.Quantity;
        }
    }
}
