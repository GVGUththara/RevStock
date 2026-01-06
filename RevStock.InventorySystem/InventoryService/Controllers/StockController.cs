using InventoryService.DTOs.Stock;
using InventoryService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("revstock/api/stocks")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _service;

        public StockController(IStockService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,InventoryOfficer,Manager,SalesPerson")]
        [HttpGet("getAllStocks")]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [Authorize(Roles = "Admin,InventoryOfficer,Manager,SalesPerson")]
        [HttpGet("getStockById/{id}")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _service.GetByIdAsync(id));
   
    }

}
