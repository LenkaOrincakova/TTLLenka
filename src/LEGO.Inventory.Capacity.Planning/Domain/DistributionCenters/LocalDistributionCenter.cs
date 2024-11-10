using System.ComponentModel.DataAnnotations;
using LEGO.Inventory.Capacity.Planning.Domain.Orders;

namespace LEGO.Inventory.Capacity.Planning.Domain.DistributionCenters;

public class LocalDistributionCenter
{
        [Key] public string name { get;set; }
    public string regionalDistributionCenterName { get; set;}
    public string finishedGoodsName { get;set; }
    public int finishedGoodsStockQuantity { get; set; }
    public int safetyStockQuantity { get; set; }
    public int safetyStockThreshold { get; set;}
    
    public LocalDistributionCenter(
        string name,
        string regionalDistributionCenterName,
        string finishedGoodsName,
        int finishedGoodsStockQuantity,
        int safetyStockQuantity,
        int safetyStockThreshold)
    {
        this.name = name;
        this.regionalDistributionCenterName = regionalDistributionCenterName;
        this.finishedGoodsName = finishedGoodsName;
        this.finishedGoodsStockQuantity = finishedGoodsStockQuantity;
        this.safetyStockQuantity = safetyStockQuantity;
        this.safetyStockThreshold = safetyStockThreshold;
    }


    
}
