using LEGO.Inventory.Capacity.Planning.Domain;
using LEGO.Inventory.Capacity.Planning.Domain.GoodsMovement;
using LEGO.Inventory.Capacity.Planning.Services.Interfaces;
using LEGO.Inventory.Capacity.Planning.Storage;

namespace LEGO.Inventory.Capacity.Planning.Services
{
    public class GoodsReceiptService : IGoodsReceiptService
    {
        private readonly IStorage _storage;
        private readonly ApiDbContext _context;

        private readonly ILogger<GoodsReceipt> _logger;


        public GoodsReceiptService(IStorage storage, ILogger<GoodsReceipt> logger, ApiDbContext context)
        {
            _storage = storage;
            _logger = logger;
            _context = context;
        }

        public List<GoodsReceipt> GetGoodsReceiptList()
        {
            return _context.GoodsReceipts.ToList();
        }

        void IGoodsReceiptService.CreateGoodsReceipt(GoodsReceipt goodsReceipt)
        {
            _context.GoodsReceipts.Add(goodsReceipt);

            var stockTransportOrder =
                _context.StockTransportOrders.FirstOrDefault(sto => sto.Id == goodsReceipt.StockTransportOrderId) ??
                throw new Exception("Missing stock transport order");

            if (stockTransportOrder.Status == StockTransportOrderStatus.Picked)
            {
                var localDistributionCenter =
                    _context.LocalDistributionCenter.First(x =>
                        x.name == stockTransportOrder.LocalDistributionCenterName);
                localDistributionCenter.safetyStockQuantity = localDistributionCenter.safetyStockThreshold;
                _logger.LogInformation(localDistributionCenter.name + "'s safety stock has been updated to " +
                                       localDistributionCenter.safetyStockQuantity);
            }
        }
    }
}