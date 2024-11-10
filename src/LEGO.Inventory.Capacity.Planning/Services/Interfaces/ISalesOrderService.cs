using LEGO.Inventory.Capacity.Planning.Domain.Orders;

namespace LEGO.Inventory.Capacity.Planning.Services.Interfaces
{
    public interface ISalesOrderService
    {
        Task CreateSalesOrderAsync(SalesOrder salesOrder);
        Task<IEnumerable<SalesOrder>> GetSalesOrdersAsync();
    }
}
