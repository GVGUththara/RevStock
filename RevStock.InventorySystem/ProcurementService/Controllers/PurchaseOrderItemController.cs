using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcurementService.DTOs.PurchaseOrderItem;
using ProcurementService.Services.Interfaces;

namespace ProcurementService.Controllers
{
    [ApiController]
    [Route("revstock/api/purchaseorder-items")]
    public class PurchaseOrderItemController : ControllerBase
    {
        private readonly IPurchaseOrderItemService _service;

        public PurchaseOrderItemController(IPurchaseOrderItemService service)
        {
            _service = service;
        }

        [Authorize(Roles = "ProcurementOfficer")]
        [HttpPost("addItem")]
        public async Task<IActionResult> Add(CreatePurchaseOrderItemDto dto)
            => await _service.AddItemAsync(dto) ? Ok() : BadRequest();

        [Authorize(Roles = "ProcurementOfficer")]
        [HttpPut("updateItem/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePurchaseOrderItemDto dto)
            => await _service.UpdateItemAsync(id, dto) ? Ok() : BadRequest();

        [Authorize(Roles = "ProcurementOfficer")]
        [HttpDelete("removeItem/{id}")]
        public async Task<IActionResult> Remove(Guid id)
            => await _service.RemoveItemAsync(id) ? Ok() : BadRequest();
    }
}
