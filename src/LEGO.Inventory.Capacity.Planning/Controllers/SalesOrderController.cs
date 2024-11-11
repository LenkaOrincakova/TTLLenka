using LEGO.Inventory.Capacity.Planning.Domain.Orders;
using LEGO.Inventory.Capacity.Planning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LEGO.Inventory.Capacity.Planning.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class SalesOrderController : ControllerBase
{

    private readonly ISalesOrderService _salesOrderService;
    private readonly IPreparationService _preparationService;
    public SalesOrderController(ISalesOrderService salesOrderService, IPreparationService preparationService)
    {
        _salesOrderService = salesOrderService;
        _preparationService = preparationService;

    }

    [HttpPost("v1/create")]
    public async Task<IActionResult> CreateSalesOrderAsync([FromBody] SalesOrder salesOrder)
    {
        try
    
        {
             _preparationService.PrepareSalesOrder(salesOrder);
            await _salesOrderService.CreateSalesOrderAsync(salesOrder);
            return Ok(salesOrder);

            
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpGet("v1/orders")]
    public async Task<IActionResult> GetSalesOrdersAsync()
    {
       var ordersDb= await _salesOrderService.GetSalesOrdersAsync();
        try
        {
            if (ordersDb is null)
            {
                return Ok($"There are no orders created");
            }

            return Ok(ordersDb);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
