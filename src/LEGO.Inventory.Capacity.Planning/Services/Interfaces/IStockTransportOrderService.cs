using LEGO.Inventory.Capacity.Planning.Domain;

namespace LEGO.Inventory.Capacity.Planning.Services.Interfaces
{
    public interface IStockTransportOrderService
    {
        Task<IEnumerable<StockTransportOrder>> GetStockTransportOrdersByLDCAsync(string localDistributionCenterName);
        Task CreateStockTransportOrderAsync(StockTransportOrder stockTransportOrder);
        Task <IEnumerable<StockTransportOrder>> GetOpenStockTransportOrdersByLDCAsync(string localDistributionCenterName);

    }
}
