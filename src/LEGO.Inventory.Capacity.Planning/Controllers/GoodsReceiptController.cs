using LEGO.Inventory.Capacity.Planning.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LEGO.Inventory.Capacity.Planning.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class GoodsReceiptController : Controller
    {
        private readonly IStorage _storage;

        public GoodsReceiptController(IStorage storage)
        {
            _storage = storage;
        }
        [HttpGet("v1/receipts")]
        public IActionResult GetAll()
        {
            var receipts = _storage.GoodsReceipts;
            if (receipts.Count == 0)
            {
                return Ok($"There are no receipts");
            }

           return Ok(receipts);

        }
    }
}
