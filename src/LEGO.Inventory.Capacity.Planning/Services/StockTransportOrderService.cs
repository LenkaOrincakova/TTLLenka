using LEGO.Inventory.Capacity.Planning.Domain;
using LEGO.Inventory.Capacity.Planning.Services.Interfaces;

namespace LEGO.Inventory.Capacity.Planning.Services
{
    public class StockTransportOrderService : IStockTransportOrderService
    {
        private readonly ApiDbContext _context;

        private StockTransportOrderStatus status;

        public StockTransportOrderService( ApiDbContext context)
        {
            _context = context;
        }

        public async Task CreateStockTransportOrderAsync(StockTransportOrder stockTransportOrder)
        {
            await _context.StockTransportOrders.AddAsync(stockTransportOrder);

             await _context.SaveChangesAsync();
        }

       public async  Task<IEnumerable<StockTransportOrder>> GetStockTransportOrdersByLDCAsync(string localDistributionCenterName)
        {
            // throw new NotImplementedException();
            return await Task.FromResult(_context.StockTransportOrders.Where(sto => sto.LocalDistributionCenterName == localDistributionCenterName).ToList());
            // return await result;

        }

       
        public async Task<IEnumerable<StockTransportOrder>> GetOpenStockTransportOrdersByLDCAsync(string localDistributionCenterName)
        {
            var stockTransportOrder = _context.StockTransportOrders;
           return await Task.FromResult(stockTransportOrder.Where(sto => (sto.LocalDistributionCenterName == localDistributionCenterName) && (sto.Status == StockTransportOrderStatus.Open)).ToList());
        }
    }

}
