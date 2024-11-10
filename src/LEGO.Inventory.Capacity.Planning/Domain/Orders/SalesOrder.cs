using System.ComponentModel.DataAnnotations;

namespace LEGO.Inventory.Capacity.Planning.Domain.Orders;

public class SalesOrder
{
    public SalesOrder(
        string finishedGoodsName,
        int quantity,
        string localDistributionCenterName)
    {
        Id = Guid.NewGuid();
        FinishedGoodsName = finishedGoodsName;
        Quantity = quantity;
        LocalDistributionCenterName = localDistributionCenterName;
    }
    [Key] public Guid Id { get; set;}

    public string FinishedGoodsName { get; set; }
    public int Quantity { get; set;}
    public string LocalDistributionCenterName { get; set;}
}
