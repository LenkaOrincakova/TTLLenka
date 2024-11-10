using System.ComponentModel.DataAnnotations;

namespace LEGO.Inventory.Capacity.Planning.Domain.DistributionCenters;

public class RegionalDistributionCenter
{
    public RegionalDistributionCenter(
        string name,
        string finishedGoodsName,
        int finishedGoodsStockQuantity)
    {
        Name = name;
        FinishedGoodsName = finishedGoodsName;
        FinishedGoodsStockQuantity = finishedGoodsStockQuantity;
    }

    [Key]public string Name { get; set;}
    public string FinishedGoodsName { get; set;}
    public int FinishedGoodsStockQuantity { get; private set; }
    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity < 0)
        {
            throw new ArgumentException("Quantity cannot be negative.");
        }

        FinishedGoodsStockQuantity = newQuantity;
    }
}
