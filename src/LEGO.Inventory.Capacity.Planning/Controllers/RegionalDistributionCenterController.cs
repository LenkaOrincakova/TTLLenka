using LEGO.Inventory.Capacity.Planning.Domain;
using LEGO.Inventory.Capacity.Planning.Domain.DistributionCenters;
using LEGO.Inventory.Capacity.Planning.Domain.GoodsMovement;
using LEGO.Inventory.Capacity.Planning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace LEGO.Inventory.Capacity.Planning.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class RegionalDistributionCenterController : Controller
    {
        private readonly IRegionalDistributionCenterService _regionalDistributionCenterService;
        private readonly IGoodsReceiptService _goodsReceiptService;
        private readonly ILogger<RegionalDistributionCenterController> _logger;
     private readonly ApiDbContext _context;
        public RegionalDistributionCenterController(IRegionalDistributionCenterService regionalDistributionCenterService, IGoodsReceiptService goodsReceiptService, ILogger<RegionalDistributionCenterController> logger, ApiDbContext context)
        {
            _regionalDistributionCenterService = regionalDistributionCenterService;
            _goodsReceiptService = goodsReceiptService;
            _logger = logger;
            _context = context;
        }
        [HttpPost("v1/pick-sto/{id}")]
        public IActionResult HandleStockTransportOrder(Guid id)
        {
            var quantityLeft = 0;
            try 
            { 
                quantityLeft = _regionalDistributionCenterService.TryPickSTO(id);
                _logger.LogInformation($"Order --"+id.ToString()+ "-- is being picked");
                _logger.LogInformation($"Quantity left: {quantityLeft}");
            } 
            catch (Exception)
            {
                if (quantityLeft == 0)
                {
                    return BadRequest($"Insufficient stock");
                }

                _logger.LogError("Error");
                return BadRequest($"wrong transport order id");
            }

            _goodsReceiptService.CreateGoodsReceipt(new GoodsReceipt(id));
            
            _logger.LogInformation($"Stock transport order --{id}"+ $"-- Has been finished");
            
            return Ok();
        }


         [HttpPost("v1/add-rdc")]
        public async Task  AddLocalDistributionCenter([FromBody] RegionalDistributionCenter rdc)
        {
             await _context.RegionalDistributionCenter.AddAsync(rdc);
             await _context.SaveChangesAsync();
        
        }
    }
}
