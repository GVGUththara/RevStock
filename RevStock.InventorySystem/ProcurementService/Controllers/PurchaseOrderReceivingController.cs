using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcurementService.DTOs.PurchaseOrder;
using ProcurementService.Services.Interfaces;

namespace ProcurementService.Controllers
{
    [ApiController]
    [Route("revstock/api/purchaseorders/receiving")]
    public class PurchaseOrderReceivingController : ControllerBase
    {
        private readonly IPurchaseOrderReceivingService _service;

        public PurchaseOrderReceivingController(IPurchaseOrderReceivingService service)
        {
            _service = service;
        }

        [Authorize(Roles = "InventoryOfficer,Manager")]
        [HttpPost("receive")]
        public async Task<IActionResult> Receive(ReceivePoItemDto dto)
            => await _service.ReceiveAsync(dto) ? Ok() : BadRequest();
    }
}
