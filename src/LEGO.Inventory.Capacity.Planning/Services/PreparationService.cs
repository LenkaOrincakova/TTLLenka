using LEGO.Inventory.Capacity.Planning.Domain;
using LEGO.Inventory.Capacity.Planning.Domain.DistributionCenters;
using LEGO.Inventory.Capacity.Planning.Domain.Orders;
using LEGO.Inventory.Capacity.Planning.Services.Interfaces;

namespace LEGO.Inventory.Capacity.Planning.Services;

public class PreparationService : IPreparationService
{
    private readonly IStockTransportOrderService _stockTransportOrderService;
    private readonly ILogger<PreparationService> _logger;
     private readonly ApiDbContext _context;


    public PreparationService(IStockTransportOrderService stockTransportOrderService, ApiDbContext context,
        ILogger<PreparationService> logger)
    {
        _context = context;
        _stockTransportOrderService = stockTransportOrderService;
        _logger = logger;
    }

    public void PrepareSalesOrder(SalesOrder salesOrder)
    {
        var _localDistributionCenter =
            _context.LocalDistributionCenter.FirstOrDefault(ldc => ldc.name == salesOrder.LocalDistributionCenterName);
        var requiredQuantity = 0;

        if(_localDistributionCenter.finishedGoodsStockQuantity >= salesOrder.Quantity)
        {
            _localDistributionCenter.finishedGoodsStockQuantity = _localDistributionCenter.finishedGoodsStockQuantity - salesOrder.Quantity;
            _logger.LogInformation(_localDistributionCenter.name+"'s new stock quantity: " + _localDistributionCenter.finishedGoodsStockQuantity);
            return;
        }
        else if (_localDistributionCenter.finishedGoodsStockQuantity < salesOrder.Quantity)
        {
            var shortfall = salesOrder.Quantity - _localDistributionCenter.finishedGoodsStockQuantity;
            _localDistributionCenter.finishedGoodsStockQuantity = 0;

            if (_localDistributionCenter.safetyStockQuantity >= shortfall)
            {
                _localDistributionCenter.safetyStockQuantity -= shortfall;
                requiredQuantity = shortfall;
            }
            else
            {
                requiredQuantity = _localDistributionCenter.safetyStockThreshold;
                _localDistributionCenter.safetyStockQuantity = 0;
            }

            if (_localDistributionCenter.safetyStockQuantity == 0)
            {
                CreateSto(salesOrder, _localDistributionCenter, requiredQuantity);
            }
            else if (_localDistributionCenter.safetyStockQuantity < _localDistributionCenter.safetyStockThreshold)
            {
                CreateSto(salesOrder, _localDistributionCenter, (_localDistributionCenter.safetyStockThreshold - _localDistributionCenter.safetyStockQuantity));
            }

            _logger.LogWarning(_localDistributionCenter.name + "'s new safety stock quantity: " + _localDistributionCenter.safetyStockQuantity);
            return;
        }

        else if (salesOrder.Quantity > _localDistributionCenter.finishedGoodsStockQuantity
        && salesOrder.Quantity <= _localDistributionCenter.finishedGoodsStockQuantity + _localDistributionCenter.safetyStockQuantity)

        {
            int requiredFromSafetyStock = salesOrder.Quantity - _localDistributionCenter.finishedGoodsStockQuantity;
            _localDistributionCenter.finishedGoodsStockQuantity = 0;
            _localDistributionCenter.safetyStockQuantity = _localDistributionCenter.safetyStockQuantity - requiredFromSafetyStock;

            if (_localDistributionCenter.safetyStockQuantity == 0
                || _localDistributionCenter.safetyStockQuantity < _localDistributionCenter.safetyStockThreshold)
            {
                CreateSto(salesOrder, _localDistributionCenter, requiredFromSafetyStock);
            }
            _logger.LogInformation(_localDistributionCenter.name + "'s new stock quantity: " + _localDistributionCenter.finishedGoodsStockQuantity);
            _logger.LogWarning(_localDistributionCenter.name + "'s new safety stock quantity: " + _localDistributionCenter.safetyStockQuantity);
        }
    }

    private void CreateSto(SalesOrder salesOrder, LocalDistributionCenter _localDistributionCenter,
        int requiredQuantity)
    {
        
        var _regionalDistributionCenter =
            _context.RegionalDistributionCenter;
        _stockTransportOrderService.CreateStockTransportOrderAsync(new StockTransportOrder(
            salesOrder.FinishedGoodsName,
            requiredQuantity,
            "LEGO European Distribution Center",
            _localDistributionCenter.name));
        
        _logger.LogInformation($"new STO created: " + salesOrder.FinishedGoodsName + "Quantity: " + requiredQuantity
                               + ", from : LEGO European Distribution Center to " +
                               _localDistributionCenter.name);
    }
}