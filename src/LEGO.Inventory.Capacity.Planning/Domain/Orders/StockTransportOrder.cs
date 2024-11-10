namespace LEGO.Inventory.Capacity.Planning.Domain;

public enum StockTransportOrderStatus
{
    Open,
    Picked
}


public class StockTransportOrder
{
    public StockTransportOrder(
        string finishedGoodsName,
        int quantity,
        string regionalDistributionCenterName,
        string localDistributionCenterName)
    {
        Id = Guid.NewGuid();
        FinishedGoodsName = finishedGoodsName;
        Quantity = quantity;
        RegionalDistributionCenterName = regionalDistributionCenterName;
        LocalDistributionCenterName = localDistributionCenterName;
    }
    

    public Guid Id { get; set;}
    public string FinishedGoodsName { get;set; }
    public int Quantity { get; set;}
    public string RegionalDistributionCenterName { get; set;}
    public string LocalDistributionCenterName { get; set;}
    public StockTransportOrderStatus Status { get; private set; }

    public void UpdateStatus(StockTransportOrderStatus status) => Status = status;
}

