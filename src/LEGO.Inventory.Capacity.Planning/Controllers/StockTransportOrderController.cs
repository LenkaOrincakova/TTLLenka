using LEGO.Inventory.Capacity.Planning.Services;
using LEGO.Inventory.Capacity.Planning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LEGO.Inventory.Capacity.Planning.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class StockTransportOrderController : ControllerBase
{
    private readonly ApiDbContext _context;

    private readonly IStockTransportOrderService _stockTransportOrderService;
    public StockTransportOrderController(IStockTransportOrderService stockTransportOrderService)
    {
        _stockTransportOrderService = stockTransportOrderService;
    }

    [HttpGet("v1/STOs")]
    public async Task<IActionResult> GetStockTransportOrdersByLDCAsync([FromQuery] string nameLDC)
    {

        var sto = await _stockTransportOrderService.GetStockTransportOrdersByLDCAsync(nameLDC);
        try
        {
            if (sto is null)
            {
                return Ok($"There are no STOs created");
            }

            return Ok(sto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpGet("v1/open-stos")]
    public async Task<IActionResult> GetAllOpenAsync([FromQuery] string nameLDC)
    {
        var openSTOs = await _stockTransportOrderService.GetOpenStockTransportOrdersByLDCAsync(nameLDC);

        return Ok(openSTOs);

    }
}
