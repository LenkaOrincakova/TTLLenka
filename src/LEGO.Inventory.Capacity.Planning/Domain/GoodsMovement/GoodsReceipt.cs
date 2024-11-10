using System.ComponentModel.DataAnnotations;

namespace LEGO.Inventory.Capacity.Planning.Domain.GoodsMovement;

public class GoodsReceipt
{
    public GoodsReceipt(Guid stockTransportOrderId)
    {
        StockTransportOrderId = stockTransportOrderId;
    }

   [Key] public Guid StockTransportOrderId { get; set;}
}